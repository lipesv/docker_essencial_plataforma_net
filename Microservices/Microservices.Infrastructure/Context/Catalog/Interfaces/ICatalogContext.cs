﻿using Microservices.Infrastructure.Context.Interfaces;
using MongoDB.Driver;

namespace Microservices.Infrastructure.Context.Catalog.Interfaces
{
    public interface ICatalogContext : IStorageContext
    {
        void AddCommand(Func<Task> func);
        IMongoCollection<T> GetCollection<T>(string name);
    }
}