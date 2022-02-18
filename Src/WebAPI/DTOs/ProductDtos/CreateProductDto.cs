using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.ProductDtos
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public bool IsOfferable { get; set; } = false;

        public bool IsSold { get; set; } = false;

        [Required]
        [RegularExpression("(https?:\\/\\/.*\\.(?:png|jpg))")]
        public string PictureUri { get; set; } // dosyanın yolunu tutacağız.

        [Required]
        public decimal Price { get; set; }

        public int? BrandId { get; set; } = null;
    
        public int? ColorId { get; set; } = null;

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int ConditionsOfProductId { get; set; }



    }
}
