using MongoDB.Driver;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Data.Context.Interfaces
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; }
    }
}