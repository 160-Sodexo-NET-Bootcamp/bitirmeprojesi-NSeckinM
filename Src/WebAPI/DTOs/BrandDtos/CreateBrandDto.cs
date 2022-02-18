using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.BrandDtos
{
    public class CreateBrandDto
    {
        [Required]
        public string BrandName { get; set; }
    }
}
