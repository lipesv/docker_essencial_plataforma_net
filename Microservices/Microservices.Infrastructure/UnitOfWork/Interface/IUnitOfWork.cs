using Catalog.Domain.Entities.Base;
using Microservices.Domain.Core.Repositories.Interfaces.Generic;
using Microservices.Infrastructure.Context;

namespace Microservices.Infrastructure.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : MongoContext
    {
        TContext Context { get; }
    }
}
