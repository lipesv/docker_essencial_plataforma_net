using Catalog.Domain.Entities;
using Microservices.Domain.Core.Repositories.Generic;

namespace Microservices.Infrastructure.Repositories.Catalog.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategory(string categoryName);

        Task<IEnumerable<Product>> GetByName(string name);
    }
}