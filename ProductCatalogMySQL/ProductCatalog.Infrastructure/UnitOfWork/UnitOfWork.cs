using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Core.Interfaces.Repositories;
using ProductCatalog.Domain.Core.Interfaces.UnitOfWork;
using ProductCatalog.Domain.Entities.Base;
using ProductCatalog.Infrastructure.Repositories.Base;

namespace ProductCatalog.Infrastructure.UnitOfWork
{
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork
        where TContext : DbContext, IDisposable
    {
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public GenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type)) _repositories[type] = new GenericRepository<TEntity>(Context);

            return (GenericRepository<TEntity>)_repositories[type];
        }

        public TContext Context { get; }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public async Task SaveSaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        
    }
}