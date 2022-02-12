﻿using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Color : BaseEntity
    {
        public string ColorName { get; set; }

        public List<Product> Products { get; set; }
    }
}
