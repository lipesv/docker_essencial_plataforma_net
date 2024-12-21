using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Core.Interfaces.Repositories;
using ProductCatalog.Domain.Entities.Base;

namespace ProductCatalog.Domain.Core.Interfaces.UnitOfWork
{
        public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;

        int SaveChanges();
        Task SaveSaveChangesAsync();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}