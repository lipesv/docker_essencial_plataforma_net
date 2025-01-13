using Catalog.Domain.Entities.Base;
using Microservices.Domain.Core.Repositories.Interfaces.Generic;
using Microservices.Infrastructure.Context.Interfaces;

namespace Microservices.Infrastructure.Repositories.Base
{
    public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected readonly IStorageContext Context;

        protected BaseRepository(IStorageContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public abstract Task<IEnumerable<TEntity>> GetAll();

        public abstract Task<TEntity> GetById(TKey id);

        public abstract void Add(TEntity entity);

        public abstract void Update(TEntity entity);

        public abstract void Delete(TKey id);

        public abstract void Dispose();
    }

}