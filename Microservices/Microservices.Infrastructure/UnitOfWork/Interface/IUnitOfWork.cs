using Catalog.Domain.Entities.Base;
using Microservices.Domain.Core.Repositories.Interfaces.Generic;
using Microservices.Infrastructure.Context.Interfaces;

namespace Microservices.Infrastructure.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IStorageContext Context { get; }
        Task<bool> Commit();
        IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class, IEntity<TKey>;
    }
}
