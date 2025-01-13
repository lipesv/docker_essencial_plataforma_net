using Catalog.Domain.Entities.Base;
using Catalog.Infrastructure.Repositories.Base;
using Microservices.Domain.Core.Repositories.Interfaces.Generic;
using Microservices.Infrastructure.Context;
using Microservices.Infrastructure.UnitOfWork.Interface;

namespace Microservices.Infrastructure.UnitOfWork
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
