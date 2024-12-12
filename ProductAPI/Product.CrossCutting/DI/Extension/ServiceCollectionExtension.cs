using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Services;
using Product.Application.Services.Interfaces;
using Product.CrossCutting.DI.Mappings;
using Product.Data.Context;
using Product.Data.Context.Interfaces;
using Product.Data.Repositories;
using Product.Data.Repositories.Interfaces;

namespace Product.CrossCutting.DI.Extension
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