using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Reflection;
using WooliesAPI.Core.Services;
using WooliesAPI.Core.ShopperHistories.Queries;
using WooliesAPI.Core.Trollies.Queries.GetTrolleyTotal;
using WooliesAPI.Core.Users.Queries.GetUser;
using WooliesAPI.Domain.Configuration;
using WooliesAPI.Domain.Entities;
using WooliesAPI.Persistence.Api;
using WooliesAPI.Persistence.Db;

namespace WooliesAPI.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppConfig>(Configuration);

            services.AddMediatR(typeof(GetUserQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetProductsQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetTrolleyTotalQueryHandler).GetTypeInfo().Assembly);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Just injecting data into our DB Context on startup
            services.AddTransient<IDbContext, WooliesApiDbContext>(options =>
            {
                var config = options.GetService<IOptions<AppConfig>>().Value;
                return new WooliesApiDbContext(new List<User>() { new User { Name = "Nick Fane", Token = config.ApiConfig.Token } });
            });
            services.AddHttpClient<IResourceApi, WooliesResourceApi>( (options, client) =>
            {
                var config = options.GetService<IOptions<AppConfig>>().Value;
                client.BaseAddress = new System.Uri(config.ApiConfig.WooliesResourceApiBaseUrl);
            });

            services.AddTransient<IProductOrderingService, ProductOrderingService>();
            services.AddTransient<IPopularProductService, PopularProductService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
