using Catalog.Application.Services;
using Catalog.Application.Services.Interfaces;
using Catalog.Domain.Core.Repositories.Interfaces;
using Catalog.Domain.Core.Repositories.Interfaces.Generic;
using Catalog.Domain.Core.Settings.MongoDbSettings;
using Catalog.Domain.Core.Settings.MongoDbSettings.Interfaces;
using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Repositories;
using Catalog.Infrastructure.Repositories.Base;
using Catalog.Infrastructure.UnitOfWork;
using Catalog.Infrastructure.UnitOfWork.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.GenericRepository.Interfaces;

namespace Catalog.CrossCutting.Extensions
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
            _services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            _services.AddScoped<IProductRepository, ProductRepository>();

            _services.AddScoped<MongoContext>();
            _services.AddScoped<IMongoContext, MongoContext>();
            _services.AddScoped<IUnitOfWork, UnitOfWork<MongoContext>>();
            _services.AddScoped<IUnitOfWork<MongoContext>, UnitOfWork<MongoContext>>();

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