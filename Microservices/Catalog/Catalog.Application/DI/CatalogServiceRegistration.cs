using Catalog.Application.Services;
using Catalog.Application.Services.Interfaces;
using Common.CrossCutting.DependencyInjection.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.DI
{
    public class CatalogServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProductService, ProductService>();
        }
    }
}