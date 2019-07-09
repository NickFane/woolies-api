using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WooliesAPI.Domain.Entities;

namespace WooliesAPI.Core.Trollies.Queries.GetTrolleyTotal
{
    public class GetTrolleyTotalQuery : IRequest<decimal>
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Special> Specials { get; set; }
        public IEnumerable<ProductQuantity> Quantities { get; set; }
    }
}
