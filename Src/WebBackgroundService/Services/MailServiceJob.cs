using ApplicationCore.Entities;
using ApplicationCore.Enum;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBackgroundService.Interfaces;

namespace WebBackgroundService.Services
{
    public class MailServiceJob :IMailServiceJob
    {

        private readonly ApplicationDbContext _dbContext;
        public MailServiceJob(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task AddMail(string mail)
        {
            Mail mail1 = new()
            {
                EmailAdress = mail,
                EmailStatus = EmailStatus.WelcomeMail,

            };
            _dbContext.Mails.Add(mail1);
            _dbContext.SaveChanges();

            return Task.FromResult(mail1);
        }
        public async Task<List<Mail>> GetAllMail()
        {
            List<Mail> mails = _dbContext.Mails.ToList();
            return mails;
        }
        public Task<List<Mail>> GetSpecificMail()
        {
            List<Mail> mails = _dbContext.Mails.Where(x => x.EmailStatus == EmailStatus.WelcomeMail && x.Creationtime == DateTime.Now).ToList();

            return Task.FromResult(mails);

        }

    }
}
