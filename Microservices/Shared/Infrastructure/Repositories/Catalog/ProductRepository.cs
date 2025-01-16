using Catalog.Domain.Entities;
using Infrastructure.Context.Mongo.Interfaces;
using Infrastructure.Repositories.Catalog.Interface;
using Microservices.Infrastructure.Repositories.Catalog;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Catalog
{
    public class ProductRepository : CatalogRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category,
                                                                           categoryName);

            return await DbSet.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name,
                                                                                  name);

            return await DbSet.Find(filter).ToListAsync();
        }
    }
}