using System;
using System.Collections.Generic;
using System.Text;

namespace WooliesAPI.Domain.Configuration
{
    public class AppConfig
    {
        public ApiConfig ApiConfig { get; set; }
    }

    public class ApiConfig
    {
        public string Token { get; set; }
        public string WooliesResourceApiBaseUrl { get; set; }
        public string ShopperHistoryEndpoint { get; set; }
        public string ProductEndpoint { get; set; }
        public string TrolleyCalculatorEndpoint { get; set; }
    }
}
