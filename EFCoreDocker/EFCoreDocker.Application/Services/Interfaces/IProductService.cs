using EFCoreDocker.Domain.Entities;

namespace EFCoreDocker.Application.Services.Interfaces
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