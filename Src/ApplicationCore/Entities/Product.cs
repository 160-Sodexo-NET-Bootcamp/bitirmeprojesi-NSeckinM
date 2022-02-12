using ApplicationCore.Common;
using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Product : BaseEntity
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string  Description { get; set; }

        public bool IsOfferable { get; set; } = false;

        public bool IsSold { get; set; } = false;

        public string PictureUri { get; set; } // dosyanın yolunu tutacağız.

        public decimal Price { get; set; }


        //Nav

        public int BrandId { get; set; }
        public Brand? Brand { get; set; }

        public int ColorId { get; set; }
        public Color? Color { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ConditionsOfProductId { get; set; }
        public ConditionsOfProduct ConditionsOfProduct { get; set; }


        public List<Offer> Offers { get; set; }


    }
}
