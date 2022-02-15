using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IMailService
    {
        
        Task AddMail(Mail mail);
        Mail GetMail(string mail);

        Task<List<Mail>> GetWelcomeMail();
        Task<List<Mail>> GetBlockMail();

    }
}
