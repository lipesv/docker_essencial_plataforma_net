using Catalog.Domain.Core.Repositories.Interfaces.Generic;
using Catalog.Domain.Entities.Base;
using Catalog.Infrastructure.Context;
using MongoDB.GenericRepository.Interfaces;

namespace Catalog.Infrastructure.UnitOfWork.Interface
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
