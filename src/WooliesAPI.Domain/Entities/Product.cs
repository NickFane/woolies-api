﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WooliesAPI.Domain.Entities
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}
