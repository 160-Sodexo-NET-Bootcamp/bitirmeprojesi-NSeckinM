using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class ConditionsOfProduct : BaseEntity
    {
        public string Condition { get; set; }

        //Nav
        public List<Product> Products { get; set; }

    }

}
