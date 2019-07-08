using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using WooliesAPI.Domain.Entities;
using WooliesAPI.Persistence.Api;

namespace WooliesAPI.Core.Services
{
    public class PopularProductService : IPopularProductService
    {
        private readonly IResourceApi _resourceApi;

        public PopularProductService(IResourceApi resourceApi)
        {
            _resourceApi = resourceApi;
        }

        public async Task<IEnumerable<ProductPopularity>> GetProductPopularityAsync()
        {
            var shoppingHistories = await _resourceApi.GetShopperHistory();

            // Build a dictionary of products keyed by name
            // With their quantities purchased as the value
            Dictionary<string, decimal> products = new Dictionary<string, decimal>();
            foreach (var history in shoppingHistories)
            {
                foreach (var product in history.Products)
                {
                    if (products.TryGetValue(product.Name, out var quantity))
                    {
                        products[product.Name] += quantity;
                    }
                    else
                    {
                        products.Add(product.Name, product.Quantity);
                    }
                }
            }

            // Iterate through the products by most purchased -> least purchased
            // Assign an incrementing rank based on quantities.
            var orderedProducts = products.OrderByDescending(o => o.Value).ToList(); //.ThenBy(o => o.Key).ToList(); // Commented out due to incorrectness? If popularity is the same, what's the consistent secondary sort? 
            var productPopularity = new List<ProductPopularity>();
            for (int i = 0; i < orderedProducts.Count; i++)
            {
                var product = orderedProducts.ElementAt(i);
                productPopularity.Add(new ProductPopularity() { ProductName = product.Key, PopularityRank = i });
            }

            return productPopularity;
        }
    }

    public interface IPopularProductService
    {
        Task<IEnumerable<ProductPopularity>> GetProductPopularityAsync();
    }
}
