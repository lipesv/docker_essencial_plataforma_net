using Common.Domain.Core.Entities;
using Common.Domain.Core.Repositories.Generic;
using Common.Infrastructure.Context.Interfaces;

namespace Common.Infrastructure.Repositories.Base
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly IStorageContext Context;

        protected BaseRepository(IStorageContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public abstract Task<IEnumerable<TEntity>> GetAll();
        public abstract Task<TEntity> GetById(object id);
        public abstract void Add(TEntity entity);
        public abstract void Update(TEntity entity);
        public abstract void Delete(object id);
        public abstract void Dispose();
    }

}