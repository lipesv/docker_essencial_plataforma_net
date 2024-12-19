using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Create(Product product);
        Task<Product> Update(Product product);
        Task<bool> Delete(int id);
    }
}