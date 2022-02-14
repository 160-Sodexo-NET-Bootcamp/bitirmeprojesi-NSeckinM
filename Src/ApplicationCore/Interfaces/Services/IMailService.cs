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

        Task AddMail(string mail);

        Task<List<Mail>> GetSpecificMail();

    }
}
