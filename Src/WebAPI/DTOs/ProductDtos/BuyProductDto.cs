using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.ProductDtos
{
    public class BuyProductDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}
