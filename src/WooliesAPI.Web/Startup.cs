using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;
using WooliesAPI.Core.Users.Queries.GetUser;
using WooliesAPI.Domain.Entities;
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
            services.AddMediatR(typeof(GetUserQueryHandler).GetTypeInfo().Assembly);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Just injecting data into our DB Context on startup
            services.AddTransient<IDbContext, WooliesApiDbContext>(options =>
            {
                return new WooliesApiDbContext(new List<User>() { new User { Name = "Nick Fane", Token = "6e424f40-80a9-49b8-8d66-5921a6734555" } });
            });
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
