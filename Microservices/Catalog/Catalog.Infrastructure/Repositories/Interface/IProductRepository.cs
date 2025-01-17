using Catalog.Domain.Entities;
using Common.Domain.Core.Repositories.Generic;

namespace Catalog.Infrastructure.Repositories.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategory(string categoryName);

        Task<IEnumerable<Product>> GetByName(string name);
    }
}