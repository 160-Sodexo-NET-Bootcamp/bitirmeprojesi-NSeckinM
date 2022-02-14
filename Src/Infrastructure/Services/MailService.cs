using ApplicationCore.Entities;
using ApplicationCore.Enum;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAsyncRepository<Mail> _mailRepository;

        public MailService(ApplicationDbContext dbContext, IAsyncRepository<Mail> mailRepository)
        {
            _dbContext = dbContext;
            _mailRepository = mailRepository;
        }


        public Task AddMail(string mail)
        {

            Mail mail1 = new()
            {
                EmailAdress = mail,
                 EmailStatus = EmailStatus.WelcomeMail,
            };

            _mailRepository.AddAsync(mail1);
            return Task.FromResult(mail1);

        }

        public async Task<List<Mail>> GetSpecificMail()
        {
            //https://localhost:44330/hangfire/recurring
            //.Where(x => x.EmailStatus == EmailStatus.WelcomeMail && x.Creationtime == DateTime.Now)
            List<Mail> mails = await _dbContext.Mails.Where(x => x.EmailStatus == EmailStatus.WelcomeMail && x.Creationtime.Date == DateTime.Today).ToListAsync();

            return mails;
        }
    }
}
