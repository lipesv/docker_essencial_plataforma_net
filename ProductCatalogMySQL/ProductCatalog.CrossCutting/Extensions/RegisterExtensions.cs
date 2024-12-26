using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Services;
using ProductCatalog.Application.Services.Interfaces;
using ProductCatalog.Domain.Core.Interfaces.Repositories;
using ProductCatalog.Domain.Core.Interfaces.UnitOfWork;
using ProductCatalog.Infrastructure.Context;
using ProductCatalog.Infrastructure.Repositories.Base;
using ProductCatalog.Infrastructure.UnitOfWork;

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

        public ServiceCollectionBuilder AddInjections()
        {
            _services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            _services.AddTransient<IProductService, ProductService>();
            _services.AddTransient<IUnitOfWork, UnitOfWork>();

            return this;
        }

        public ServiceCollectionBuilder AddDbContexts(string connectionString)
        {
            // var connStr = string.Format(connectionString,
            //                             args: [
            //                                 Environment.GetEnvironmentVariable("db_uid"),
            //                                 Environment.GetEnvironmentVariable("db_pwd"),
            //                                 Environment.GetEnvironmentVariable("db")
            //                             ]);

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

    public static class RegisterExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            new ServiceCollectionBuilder(services)
                .AddConfiguration(configuration)
                .AddInjections()
                .AddDbContexts(configuration.GetConnectionString("DefaultConnection"))
                .Build();
        }
    }
}