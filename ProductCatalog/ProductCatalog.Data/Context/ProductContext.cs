using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ProductCatalog.Data.Context.Interfaces;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Data.Context
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IConfiguration configuration)
        {
            var connStr = string.Format(configuration["DatabaseSettings:ConnectionString"],
                                                       Environment.GetEnvironmentVariable("DbContainer") ?? "localhost");

            var client = new MongoClient(connStr);

            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

            Products = database.GetCollection<Product>(configuration["DatabaseSettings:CollectionName"]);

            ProductContextSeed.SeedData(Products);
        }

        public IMongoCollection<Domain.Entities.Product> Products { get; }
    }
}