using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WooliesAPI.Domain.Configuration;
using WooliesAPI.Domain.Entities;

namespace WooliesAPI.Persistence.Api
{
    public class WooliesResourceApi : IResourceApi
    {
        private readonly HttpClient _client;
        private readonly AppConfig _config;

        public WooliesResourceApi(HttpClient client, IOptions<AppConfig> config)
        {
            _client = client;
            _config = config.Value;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            // Would prefer to bake the token into our client if time wasn't a factor
            var result = await _client.GetStringAsync($"{_config.ApiConfig.ProductEndpoint}?token={_config.ApiConfig.Token}");
            return JsonConvert.DeserializeObject<IEnumerable<Product>>(result);
        }

        public async Task<IEnumerable<ShopperHistory>> GetShopperHistory()
        {
            var result = await _client.GetStringAsync($"{_config.ApiConfig.ShopperHistoryEndpoint}?token={_config.ApiConfig.Token}");
            return JsonConvert.DeserializeObject<IEnumerable<ShopperHistory>>(result);
        }

        public async Task<decimal> GetTrolleyTotalAsync(TrolleyCalculatorQuery query)
        {
            var body = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");

            var result = await _client.PostAsync($"{_config.ApiConfig.TrolleyCalculatorEndpoint}?token={_config.ApiConfig.Token}", body);
            return JsonConvert.DeserializeObject<decimal>(await result.Content.ReadAsStringAsync());
        }
    }

    public interface IResourceApi
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<ShopperHistory>> GetShopperHistory();
        Task<decimal> GetTrolleyTotalAsync(TrolleyCalculatorQuery trolleyTotalQuery);
    }


}
