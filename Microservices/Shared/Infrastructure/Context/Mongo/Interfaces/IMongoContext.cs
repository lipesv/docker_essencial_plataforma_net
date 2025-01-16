using Infrastructure.Context.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Context.Mongo.Interfaces
{
    public interface IMongoContext : IStorageContext
    {
        void AddCommand(Func<Task> func);
        IMongoCollection<T> GetCollection<T>(string name);
    }
}