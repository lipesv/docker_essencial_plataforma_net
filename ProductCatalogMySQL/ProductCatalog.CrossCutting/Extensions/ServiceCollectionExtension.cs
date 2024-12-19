using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Services;
using ProductCatalog.Application.Services.Interfaces;
using ProductCatalog.Data.Context;
using ProductCatalog.Data.Repositories;
using ProductCatalog.Data.Repositories.Interfaces;
using ProductCatalog.Data.UnitOfWork;
using ProductCatalog.Data.UnitOfWork.Interfaces;

namespace ProductCatalog.CrossCutting.Extensions
{
    public class ServiceCollectionBuilder
    {
        private readonly IServiceCollection _services;

        public ServiceCollectionBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public ServiceCollectionBuilder AddConfiguration(IConfiguration configuration)
        {
            _services.AddSingleton<IConfiguration>(configuration);
            return this;
        }

        public ServiceCollectionBuilder AddServices()
        {
            _services.AddTransient<IProductService, ProductService>();
            _services.AddScoped<IUnitOfWork, UnitOfWork>();
            _services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return this;
        }

        public ServiceCollectionBuilder AddDataBaseContext(string connectionString)
        {
            _services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            
            return this;
        }

        public IServiceCollection Build()
        {
            return _services;
        }
    }

    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            new ServiceCollectionBuilder(services)
                .AddConfiguration(configuration)
                .AddServices()
                .AddDataBaseContext(configuration.GetConnectionString("DefaultConnection"))
                .Build();
        }
    }
}