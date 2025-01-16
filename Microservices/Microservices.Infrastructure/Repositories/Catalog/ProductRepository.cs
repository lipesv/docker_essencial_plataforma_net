using Catalog.Domain.Entities;
using Microservices.Infrastructure.Context.Catalog.Interfaces;
using Microservices.Infrastructure.Repositories.Catalog.Base;
using Microservices.Infrastructure.Repositories.Catalog.Interface;
using MongoDB.Driver;

namespace Microservices.Infrastructure.Repositories
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