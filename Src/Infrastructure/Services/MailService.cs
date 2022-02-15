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


        public Task AddMail(Mail mail)
        {
            _mailRepository.AddAsync(mail);
            return Task.FromResult(mail);
        }
        public Mail GetMail(string mail)
        {
            Mail forblockmail = _dbContext.Mails.FirstOrDefault(x => x.EmailAdress.Equals(mail));
            return forblockmail;
        }

        public async Task<List<Mail>> GetBlockMail()
        {
            List<Mail> mails = await _dbContext.Mails.Where(x => x.EmailStatus == EmailStatus.BlockMail).ToListAsync();

            return mails;
        }

        public async Task<List<Mail>> GetWelcomeMail()
        {
            //https://localhost:44330/hangfire/recurring
            List<Mail> mails = await _dbContext.Mails.Where(x => x.EmailStatus == EmailStatus.WelcomeMail && x.Creationtime.Date == DateTime.Today).ToListAsync();

            return mails;
        }
    }
}
