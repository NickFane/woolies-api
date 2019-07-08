using System;
using System.Collections.Generic;
using System.Text;

namespace WooliesAPI.Domain.Entities
{
    public class ShopperHistory
    {
        public int CustomerId { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
