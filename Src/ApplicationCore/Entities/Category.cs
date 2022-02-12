using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }


        //Nav
        public List<Product> Products { get; set; }

    }
}
