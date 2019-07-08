using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WooliesAPI.Domain.Entities;

namespace WooliesAPI.Persistence.Api
{
    public class WooliesResourceApi : IResourceApi
    {
        private readonly HttpClient _client;

        // TODO add to configuration
        private readonly string _token = "6e424f40-80a9-49b8-8d66-5921a6734555";

        public WooliesResourceApi(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var result = await _client.GetStringAsync($"/api/resource/products?token={_token}");
            return JsonConvert.DeserializeObject<IEnumerable<Product>>(result);
        }

        public async Task<IEnumerable<ShopperHistory>> GetShopperHistory()
        {
            var result = await _client.GetStringAsync($"/api/resource/shopperHistory?token={_token}");
            return JsonConvert.DeserializeObject<IEnumerable<ShopperHistory>>(result);
        }
    }

    public interface IResourceApi
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<ShopperHistory>> GetShopperHistory();
    }
}
