using System;
using System.Collections.Generic;
using System.Text;

namespace WooliesAPI.Domain.Entities
{
    public class TrolleyCalculatorQuery
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Special> Specials { get; set; }
        public IEnumerable<ProductQuantity> Quantities { get; set; }
    }
}
