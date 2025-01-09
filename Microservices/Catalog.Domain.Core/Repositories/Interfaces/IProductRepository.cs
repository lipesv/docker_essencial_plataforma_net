using Catalog.Domain.Core.Repositories.Interfaces.Generic;
using Catalog.Domain.Entities;

namespace Catalog.Domain.Core.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategory(string categoryName);

        Task<IEnumerable<Product>> GetByName(string name);
    }
}