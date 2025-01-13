using Catalog.Domain.Entities;
using Microservices.Domain.Core.Repositories.Interfaces.Generic;

namespace Microservices.Domain.Core.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategory(string categoryName);

        Task<IEnumerable<Product>> GetByName(string name);
    }
}