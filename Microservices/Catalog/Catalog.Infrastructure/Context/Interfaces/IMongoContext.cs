using Common.Infrastructure.Context.Interfaces;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Context.Interfaces
{
    public interface IMongoContext : IStorageContext
    {
        void AddCommand(Func<Task> func);
        IMongoCollection<T> GetCollection<T>(string name);
    }
}