using Catalog.Domain.Core.Repositories.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Repositories.Base;
using MongoDB.Driver;
using MongoDB.GenericRepository.Interfaces;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
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