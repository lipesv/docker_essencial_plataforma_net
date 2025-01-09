using Catalog.Domain.Core.Repositories.Interfaces.Generic;
using Catalog.Domain.Entities.Base;
using Catalog.Infrastructure.Repositories.Base;
using Catalog.Infrastructure.UnitOfWork.Interface;
using MongoDB.GenericRepository.Interfaces;

namespace Catalog.Infrastructure.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable where TContext : IMongoContext
    {
        private Dictionary<Type, object> _repositories;
        private bool disposed = false;

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type)) _repositories[type] = new BaseRepository<TEntity>(Context);

            return (IRepository<TEntity>)_repositories[type];
        }

        public TContext Context { get; }

        public async Task<bool> Commit()
        {
            var changeAmount = await Context.SaveChanges();

            return changeAmount > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                Context?.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
