using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Services;
using ProductCatalog.Application.Services.Interfaces;
using ProductCatalog.Data.Context;
using ProductCatalog.Data.Context.Interfaces;
using ProductCatalog.Data.Repositories;
using ProductCatalog.Data.Repositories.Interfaces;

namespace ProductCatalog.CrossCutting.DI.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void InjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);

            services.AddTransient<IProductService, ProductService>();
            services.AddScoped<IProductContext, ProductContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}