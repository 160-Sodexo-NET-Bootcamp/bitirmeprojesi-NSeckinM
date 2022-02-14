using ApplicationCore.Entities;
using ApplicationCore.Enum;
using ApplicationCore.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WebBackgroundService.Interfaces;

namespace WebBackgroundService.HangFireJobs
{
    public class AutoMailSender
    {
        private readonly IMailServiceJob _mailServiceJob;

        public AutoMailSender(IMailServiceJob mailServiceJob)
        {
            _mailServiceJob = mailServiceJob;
        }
        public async void SendMail()
        {
            List<Mail> mails = await _mailServiceJob.GetSpecificMail();

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
