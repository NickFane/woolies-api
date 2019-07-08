using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooliesAPI.Domain.Entities;
using WooliesAPI.Domain.Enums;
using WooliesAPI.Persistence.Api;

namespace WooliesAPI.Core.Services
{
    public class ProductOrderingService : IProductOrderingService
    {
        private readonly IPopularProductService _popularProductService;

        public ProductOrderingService(IPopularProductService popularProductService)
        {
            _popularProductService = popularProductService;
        }

        public async Task<IEnumerable<Product>> OrderProductsAsync(IEnumerable<Product> products, SortOption sortOption)
        {
            switch (sortOption)
            {
                case SortOption.Low:
                    return products.OrderBy(p => p.Price);
                case SortOption.High:
                    return products.OrderByDescending(p => p.Price);
                case SortOption.Ascending:
                    return products.OrderBy(p => p.Name);
                case SortOption.Descending:
                    return products.OrderByDescending(p => p.Name);
                case SortOption.Recommended:
                    var productPopularity = await _popularProductService.GetProductPopularityAsync();
                    // Fair bit of complexity here
                    // Order by the product's popularity rank
                    // If no rank, use int.MaxValue, then by name (for consistency in results with no popularity)
                    return products.OrderBy(o => productPopularity.SingleOrDefault(p => p.ProductName == o.Name)?.PopularityRank ?? int.MaxValue).ThenBy(o => o.Name);
                case SortOption.None:
                default:
                    break;
            }

            return products;
        }
    }

    public interface IProductOrderingService
    {
        Task<IEnumerable<Product>> OrderProductsAsync(IEnumerable<Product> products, SortOption sortOption);
    }
}
