using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class User : IdentityUser
    {

        public string NickName { get; set; }

        public string FullName { get; set; }

        public bool RoleType { get; set; }

    }
}
