using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.ConditionsOfProductDtos
{
    public class CreateConditionDto
    {
        [Required]
        public string Condition { get; set; }


    }
}
