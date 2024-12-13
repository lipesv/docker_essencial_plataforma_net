using MongoDB.Driver;
using ProductCatalog.Data.Context.Interfaces;
using ProductCatalog.Data.Repositories.Interfaces;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext _context;

        public ProductRepository(IProductContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                return await _context.Products.Find(p => true)
                                          .ToListAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetProduct(string id)
        {
            try
            {
                return await _context.Products.Find(p => p.Id == id)
                                          .FirstOrDefaultAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter
                                                                                                .ElemMatch(p => p.Name, name);

                return await _context.Products.Find(filter).ToListAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter
                                                                                                .Eq(p => p.Category, categoryName);

                return await _context.Products.Find(filter).ToListAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public async Task CreateProduct(Product product)
        {
            try
            {
                await _context.Products.InsertOneAsync(product);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                var updateResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id,
                                                                       replacement: product);

                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteProduct(string id)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

                DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);

                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}