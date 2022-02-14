using ApplicationCore.Entities;
using ApplicationCore.Enum;
using ApplicationCore.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace HangFire.Jobs
{
    public class MailSender
    {
        private readonly IUnitOfWork _unitOfWork;

        public MailSender(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }


        public async void SendMail()
        {
            List<Mail> mails = await _unitOfWork.MailService.GetSpecificMail();

            foreach (var item in mails)
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
                            smtp.Credentials = new NetworkCredential("seckinmantar@gmail.com", "");
                            smtp.EnableSsl = true;
                            try
                            {
                                smtp.Send(posta);
                            }
                            catch (Exception)
                            {

                                item.CoutOfTry++;
                            }
                        }
                    }
                    else
                    {
                        item.EmailStatus = EmailStatus.UnreachableMail;
                    }


                
            }
            

        }

    }
}
