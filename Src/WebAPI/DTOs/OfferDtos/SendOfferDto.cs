using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.OfferDtos
{
    public class SendOfferDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int PercentageOfOffer { get; set; }

    }
}
