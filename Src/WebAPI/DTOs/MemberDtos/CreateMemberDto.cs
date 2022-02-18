using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.MemberDtos
{
    public class CreateMemberDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string NickName { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password is not valid:  It should contain at least one upper case one lower case letter and one digit and  one special character and a Minimum 8 in length should be")]
        public string Password { get; set; }
    }
}
