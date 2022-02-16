using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Offer : BaseEntity
    {
        public Offer()
        {
            OfferedValue = Product.Price - (Product.Price * PercentageOfOffer) / 100;
        }
        public string UserId { get; set; }

        public string StatusOfOffer { get; set; }

        public int PercentageOfOffer { get; set; }

        public decimal OfferedValue { get; set; }


        //Nav
        public int ProductId { get; set; }
        public Product Product { get; set; }


    }
}
