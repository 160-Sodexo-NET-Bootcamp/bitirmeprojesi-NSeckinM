using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.OfferDtos
{
    public class SendOfferDto
    {
        public int ProductId { get; set; }
        public int PercentageOfOffer { get; set; }

    }
}
