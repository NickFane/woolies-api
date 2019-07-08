using MediatR;
using System.Collections.Generic;
using WooliesAPI.Domain.Entities;
using WooliesAPI.Domain.Enums;

namespace WooliesAPI.Core.ShopperHistories.Queries
{
    public class GetProductsQuery : IRequest<List<Product>>
    {
        public GetProductsQuery(SortOption sortOption)
        {
            SortOption = sortOption;
        }
        public SortOption SortOption { get; set; }
    }
}
