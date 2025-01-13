using Catalog.Domain.Entities;
using Microservices.Infrastructure.Context.Interfaces;
using Microservices.Infrastructure.Context.Util;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Microservices.Infrastructure.Context
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var connStr = string.Format(configuration["DatabaseSettings:ConnectionString"],
                                        Environment.GetEnvironmentVariable("DB_CONTAINER") ?? "localhost");

            var client = new MongoClient(connStr);

            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

            Products = database.GetCollection<Product>(configuration["DatabaseSettings:CollectionName"]);

            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}