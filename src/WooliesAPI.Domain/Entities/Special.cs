using System;
using System.Collections.Generic;
using System.Text;

namespace WooliesAPI.Domain.Entities
{
    public class Special
    {
        public List<ProductQuantity> Quantities { get; set; }
        public decimal Total { get; set; }
    }
}
