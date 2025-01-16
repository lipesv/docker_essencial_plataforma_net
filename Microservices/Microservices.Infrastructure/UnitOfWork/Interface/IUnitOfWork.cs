using Microservices.Domain.Core.Entities;
using Microservices.Domain.Core.Repositories.Generic;
using Microservices.Infrastructure.Context.Interfaces;

namespace Microservices.Infrastructure.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

    }
}
