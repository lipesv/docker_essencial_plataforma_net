using Catalog.Domain.Entities.Base;
using Microservices.Domain.Core.Repositories.Interfaces.Generic;
using Microservices.Infrastructure.Context.Catalog.Interfaces;
using Microservices.Infrastructure.Context.Interfaces;
using Microservices.Infrastructure.Repositories.Catalog;
using Microservices.Infrastructure.UnitOfWork.Interface;

namespace Microservices.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<Type, object> _repositories;
        private bool _disposed = false;

        public UnitOfWork(IStorageContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class, IEntity<TKey>
        {
            var entityType = typeof(TEntity);

            if (!_repositories.ContainsKey(entityType))
            {
                var repositoryType = Context switch
                {
                    ICatalogContext => typeof(CatalogRepository<,>),
                    // SqlContext => typeof(SqlRepository<,>),
                    _ => throw new NotSupportedException($"Unsupported context type: {Context.GetType()}")
                };

                var repositoryInstance = Activator.CreateInstance(
                    repositoryType.MakeGenericType(typeof(TEntity), typeof(TKey)),
                    Context);

                _repositories[entityType] = repositoryInstance;
            }

            return (IRepository<TEntity, TKey>)_repositories[entityType];
        }

        public IStorageContext Context { get; }

        public async Task<bool> Commit()
        {
            var changeAmount = await Context.SaveChanges();

            return changeAmount > 0;
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
