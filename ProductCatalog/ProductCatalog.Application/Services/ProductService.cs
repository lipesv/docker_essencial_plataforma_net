
using MongoDB.Bson;
using ProductCatalog.Application.Services.Interfaces;
using ProductCatalog.Data.Repositories.Interfaces;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> GetProductAsync(string id)
        {
            try
            {
                return await _repository.GetProduct(id);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(string categoryName)
        {
            try
            {
                return await _repository.GetProductByCategory(categoryName);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            try
            {
                return await _repository.GetProductByName(name);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try
            {
                return await _repository.GetProducts();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task Create(Product product)
        {
            try
            {
                if (string.IsNullOrEmpty(product.Id))
                {
                    product.Id = ObjectId.GenerateNewId().ToString();
                }
                
                await _repository.CreateProduct(product);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                return await _repository.DeleteProduct(id);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<bool> Update(Product product)
        {
            try
            {
                return _repository.UpdateProduct(product);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}