using Catalog.Domain.Entities;

namespace Catalog.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetByName(string name);
        Task<IEnumerable<Product>> GetByCategory(string categoryName);
        Task<bool> Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(string id);
    }
}