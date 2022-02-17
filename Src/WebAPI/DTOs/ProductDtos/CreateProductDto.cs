using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.ProductDtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsOfferable { get; set; } = false;

        public bool IsSold { get; set; } = false;

        public string PictureUri { get; set; } // dosyanın yolunu tutacağız.

        public decimal Price { get; set; }

        public int? BrandId { get; set; } = null;
    
        public int? ColorId { get; set; } = null;

        public int CategoryId { get; set; }

        public int ConditionsOfProductId { get; set; }




    }
}
