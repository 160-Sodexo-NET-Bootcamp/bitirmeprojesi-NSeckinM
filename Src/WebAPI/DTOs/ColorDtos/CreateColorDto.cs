using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.ColorDtos
{
    public class CreateColorDto
    {
        [Required]
        public string ColorName { get; set; }

    }
}
