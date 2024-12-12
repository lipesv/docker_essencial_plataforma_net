using MongoDB.Driver;

namespace Product.Data.Context.Interfaces
{
    public interface IProductContext
    {
        IMongoCollection<Domain.Entities.Product> Products { get; }
    }
}