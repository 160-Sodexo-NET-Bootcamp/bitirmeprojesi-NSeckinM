using ApplicationCore.Entities;
using ApplicationCore.Enum;
using ApplicationCore.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Hangfire;

namespace WebAPI.HangFireJobs
{
    public class AutoMailSender2
    {

        private readonly IUnitOfWork _unitOfWork;

        public AutoMailSender2(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SendBlockMail()
        {
            //List<Mail> Welcomemails = await _unitOfWork.MailService.GetWelcomeMail();
            List<Mail> Blockmails = await _unitOfWork.MailService.GetBlockMail();

            foreach (var item in Blockmails)
            {
                if (item.CoutOfTry < 5)
                {
                    string sender = "yoneticisodexo@gmail.com";//kullanıcı adı
                    string to = item.EmailAdress;
                    string subject = "Welcome";
                    string body = "Dear User, \n" + "This Account locked out for 3 days due to 3 times wrongs login attempts";
                    MailMessage posta = new MailMessage(sender, to, subject, body);
                    posta.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))//gönderici maili
                    {
                        smtp.Credentials = new NetworkCredential("yoneticisodexo@gmail.com", "Sodexo123*");//yıldız yerine parola girilmeli
                        smtp.EnableSsl = true;
                        try
                        {
                            smtp.Send(posta);
                            item.EmailStatus = EmailStatus.member;
                            _unitOfWork.Complete();
                        }
                        catch (Exception)
                        {
                            item.CoutOfTry++;
                            _unitOfWork.Complete();
                        }
                    }
                }
                else
                {
                    item.EmailStatus = EmailStatus.UnreachableMail;
                    _unitOfWork.Complete();
                }
            }
        }
    }




}

