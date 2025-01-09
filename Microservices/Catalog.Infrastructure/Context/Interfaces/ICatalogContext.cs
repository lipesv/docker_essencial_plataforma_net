using Catalog.Domain.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Context.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}