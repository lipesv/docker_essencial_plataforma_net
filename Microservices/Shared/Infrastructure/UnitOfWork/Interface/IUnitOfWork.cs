using Common.Domain.Core.Entities;
using Common.Domain.Core.Repositories.Generic;

namespace Common.Infrastructure.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

    }
}
