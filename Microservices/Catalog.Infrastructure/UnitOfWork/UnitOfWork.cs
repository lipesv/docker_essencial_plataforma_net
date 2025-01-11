using Catalog.Domain.Core.Repositories.Interfaces.Generic;
using Catalog.Domain.Entities.Base;
using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Repositories.Base;
using Catalog.Infrastructure.UnitOfWork.Interface;

namespace Catalog.Infrastructure.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : MongoContext
    {
        private Dictionary<Type, object> _repositories;
        private bool _disposed = false;

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


        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
