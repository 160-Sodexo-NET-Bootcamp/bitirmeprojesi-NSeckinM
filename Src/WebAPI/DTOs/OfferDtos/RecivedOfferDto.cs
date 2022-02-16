using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.OfferDtos
{
    public class RecivedOfferDto
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<Offer> Offers { get; set; }
        

    }
}
