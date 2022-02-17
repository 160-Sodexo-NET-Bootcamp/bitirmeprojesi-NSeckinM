using ApplicationCore.Entities;
using ApplicationCore.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using System.Net.Mail;
using System.Net;
using ApplicationCore.Enum;

namespace WebAPI.HangFireJobs
{
    public class AutoMailSender
    {
        private readonly IUnitOfWork _unitOfWork;

        public AutoMailSender(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SendWelcomeMail()
        {
            List<Mail> Welcomemails = await _unitOfWork.MailService.GetWelcomeMail();
           // List<Mail> Blockmails = await _unitOfWork.MailService.GetBlockMail();

            foreach (var item in Welcomemails)
            {
                if (item.CoutOfTry < 5)
                {
                    string sender = "seckinmantar@gmail.com";//kullanıcı adı
                    string to = item.EmailAdress;
                    string subject = "Welcome";
                    string body = "Dear User, \n" + "we are happy to see you among us";
                    MailMessage posta = new MailMessage(sender, to, subject, body);
                    posta.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))//gönderici maili
                    {
                        smtp.Credentials = new NetworkCredential("seckinmantar@gmail.com", "seckinmntr");//yıldız yerine parola girilmeli
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
