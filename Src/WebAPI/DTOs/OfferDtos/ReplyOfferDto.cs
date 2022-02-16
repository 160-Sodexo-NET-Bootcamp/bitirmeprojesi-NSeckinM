using ApplicationCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.OfferDtos
{
    public class ReplyOfferDto
    {

        public int OfferId { get; set; }
        public OfferStatus Reply { get; set; }

    }
}
