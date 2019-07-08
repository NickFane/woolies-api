using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WooliesAPI.Domain.Entities;
using WooliesAPI.Domain.Enums;
using WooliesAPI.Persistence.Api;
using WooliesAPI.Core.Services;

namespace WooliesAPI.Core.ShopperHistories.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private readonly IResourceApi _resourceApi;
        private readonly IProductOrderingService _productOrderingService;

        public GetProductsQueryHandler(IResourceApi resourceApi, IProductOrderingService productOrderingService)
        {
            _resourceApi = resourceApi;
            _productOrderingService = productOrderingService;
        }

        public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _resourceApi.GetProducts();

            var orderedProducts = await _productOrderingService.OrderProductsAsync(products, request.SortOption);
            return orderedProducts.ToList();
        }
    }
}
