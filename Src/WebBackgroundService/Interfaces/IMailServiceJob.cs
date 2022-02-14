using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBackgroundService.Interfaces
{
    public interface IMailServiceJob
    {
        Task<List<Mail>> GetAllMail();

        Task AddMail(string mail);

        Task<List<Mail>> GetSpecificMail();
    }
}
