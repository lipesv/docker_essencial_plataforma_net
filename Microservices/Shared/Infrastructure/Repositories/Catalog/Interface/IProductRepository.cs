using Catalog.Domain.Entities;
using Domain.Core.Repositories.Generic;

namespace Infrastructure.Repositories.Catalog.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategory(string categoryName);

        Task<IEnumerable<Product>> GetByName(string name);
    }
}