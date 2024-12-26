using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Core.Interfaces.Repositories;
using ProductCatalog.Domain.Entities.Base;

namespace ProductCatalog.Domain.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int Commit();
        /// <returns>The number of objects in an Added, Modified, or Deleted state asynchronously</returns>
        Task<int> CommitAsync();
        /// <returns>Repository</returns>
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }
}