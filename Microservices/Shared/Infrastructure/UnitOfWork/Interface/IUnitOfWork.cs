using Domain.Core.Entities;
using Domain.Core.Repositories.Generic;

namespace Infrastructure.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

    }
}
