using Product.Application.Services.Interfaces;
using Product.Data.Repositories.Interfaces;

namespace Product.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Product> GetProductAsync(string id)
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

        public async Task<IEnumerable<Domain.Entities.Product>> GetByCategoryAsync(string categoryName)
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

        public async Task<IEnumerable<Domain.Entities.Product>> GetByNameAsync(string name)
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

        public async Task<IEnumerable<Domain.Entities.Product>> GetProductsAsync()
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

        public async Task Create(Domain.Entities.Product product)
        {
            try
            {
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

        public Task<bool> Update(Domain.Entities.Product product)
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