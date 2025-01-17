using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Context.Interfaces;
using Catalog.Infrastructure.Repositories;
using Catalog.Infrastructure.Repositories.Base;
using Catalog.Infrastructure.Repositories.Interface;
using Common.CrossCutting.DependencyInjection.Repository;
using Common.CrossCutting.DependencyInjection.Services;
using Common.Domain.Core.Repositories.Generic;
using Common.Domain.Core.Settings.MongoDbSettings;
using Common.Domain.Core.Settings.MongoDbSettings.Interfaces;
using Common.Infrastructure.Context.Interfaces;
using Common.Infrastructure.UnitOfWork;
using Common.Infrastructure.UnitOfWork.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Catalog.Infrastructure.DI
{
    public class CatalogRepositoryRegistration : IServiceRegistration, IRepositoryRegistration
    {
        public void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
        {
            // Register specific repository types
            services.AddScoped(typeof(IRepository<>), typeof(CatalogRepository<>));

            // Register concrete repositories
            services.AddScoped<IProductRepository, ProductRepository>();

            // Register context classes
            services.AddScoped<MongoContext>();
            services.AddScoped<IMongoContext, MongoContext>();

            // Register concrete IStorageContext implementation
            services.AddScoped<IStorageContext, MongoContext>();

            // Register UnitOfWork class
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("DatabaseSettings"));

            services.AddSingleton<IMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
        }
    }
}