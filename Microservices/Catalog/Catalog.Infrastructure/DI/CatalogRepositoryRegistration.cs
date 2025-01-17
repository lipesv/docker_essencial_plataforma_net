using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Context.Interfaces;
using Catalog.Infrastructure.Repositories;
using Catalog.Infrastructure.Repositories.Base;
using Catalog.Infrastructure.Repositories.Interface;
using Common.CrossCutting.DependencyInjection.Repository;
using Common.Domain.Core.Repositories.Generic;
using Common.Infrastructure.Context.Interfaces;
using Common.Infrastructure.UnitOfWork;
using Common.Infrastructure.UnitOfWork.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.DI
{
    public class CatalogRepositoryRegistration : IRepositoryRegistration
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
    }
}