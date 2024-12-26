using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Create(Product product);
        Task Create(IEnumerable<Product> products);
        Task<bool> Update(Product product);
        Task<bool> Delete(int id);
    }
}