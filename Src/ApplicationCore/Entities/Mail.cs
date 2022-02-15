using ApplicationCore.Common;
using ApplicationCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Mail : BaseEntity
    {

        public string EmailAdress { get; set; }
        public EmailStatus EmailStatus { get; set; } = EmailStatus.WelcomeMail;
        public int CoutOfTry { get; set; } = 0;
        public DateTime Creationtime { get; set; } = DateTime.Now;

    }
}
