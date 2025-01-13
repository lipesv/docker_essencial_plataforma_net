using Catalog.Domain.Entities;
using MongoDB.Driver;

namespace Microservices.Infrastructure.Context.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}