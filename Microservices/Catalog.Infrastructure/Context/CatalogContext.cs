using Catalog.Domain.Entities;
using Catalog.Infrastructure.Context.Interfaces;
using Catalog.Infrastructure.Context.Util;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Context
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var connStr = string.Format(configuration["DatabaseSettings:ConnectionString"],
                                        Environment.GetEnvironmentVariable("DbContainer") ?? "localhost");

            var client = new MongoClient(connStr);

            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

            Products = database.GetCollection<Product>(configuration["DatabaseSettings:CollectionName"]);

            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}