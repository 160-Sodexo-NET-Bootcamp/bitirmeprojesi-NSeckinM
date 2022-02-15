using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.ProductDtos
{
    public class UpdateProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsOfferable { get; set; } = false;

        public bool IsSold { get; set; } = false;

        public string PictureUri { get; set; } // dosyanın yolunu tutacağız.

        public decimal Price { get; set; }

        public int? BrandId { get; set; }

        public int? ColorId { get; set; }

        public int CategoryId { get; set; }

        public int ConditionsOfProductId { get; set; }
    }
}
