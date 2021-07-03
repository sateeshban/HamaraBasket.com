using HamaraBasket.Com.Interfaces;
using HamaraBasket.Com.Models;
using HamaraBasket.Com.Repository;
using HamaraBasket.Com.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace HamaraBasket.Com
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IDataRetriever<Items>, ItemsDataRepository>();
            services.AddSingleton<IDataRetriever<ItemTypes>, ItemTypeDataRepository>();
            services.AddSingleton<IRuleEngine<List<Items>>, QualityRuleEngine>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
           name: "HamaraBasket",
           pattern: "{controller=HamaraBasket}/{action=Get}/{id?}");


            });

        }
    }
}
