using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Product.Data.Context.Interfaces;

namespace Product.Data.Context
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);

            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

            Products = database.GetCollection<Domain.Entities.Product>(configuration["DatabaseSettings:CollectionName"]);

            ProductContextSeed.SeedData(Products);
        }

        public IMongoCollection<Domain.Entities.Product> Products { get; }
    }
}