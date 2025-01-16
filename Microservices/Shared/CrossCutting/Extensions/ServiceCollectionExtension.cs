using Catalog.Application.Services;
using Catalog.Application.Services.Interfaces;
using Domain.Core.Repositories.Generic;
using Domain.Core.Settings.MongoDbSettings.Interfaces;
using Infrastructure.Context.Interfaces;
using Infrastructure.Context.Mongo;
using Infrastructure.Context.Mongo.Interfaces;
using Infrastructure.Repositories.Catalog;
using Infrastructure.Repositories.Catalog.Interface;
using Infrastructure.UnitOfWork;
using Infrastructure.UnitOfWork.Interface;
using Microservices.Domain.Core.Settings.MongoDbSettings;
using Microservices.Infrastructure.Repositories.Catalog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CrossCutting.Extensions
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
            _services.Configure<MongoDbSettings>(configuration.GetSection("DatabaseSettings"));

            return this;
        }

        public ServiceCollectionBuilder AddServices()
        {
            _services.AddTransient<IProductService, ProductService>();

            _services.AddSingleton<IMongoDbSettings>(serviceProvider =>
            {
                return serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            });

            return this;
        }

        public ServiceCollectionBuilder AddDataBaseContext(string connectionString)
        {
            // Register specific repository types
            _services.AddScoped(typeof(IRepository<>), typeof(CatalogRepository<>));

            // Register concrete repositories
            _services.AddScoped<IProductRepository, ProductRepository>();

            // Register context classes
            _services.AddScoped<MongoContext>();
            _services.AddScoped<IMongoContext, MongoContext>();

            // Register concrete IStorageContext implementation
            _services.AddScoped<IStorageContext, MongoContext>();

            // Register UnitOfWork class
            _services.AddScoped<IUnitOfWork, UnitOfWork>();

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
                .AddDataBaseContext(configuration.GetConnectionString("SqlConnection"))
                .Build();
        }
    }
}