using Catalog.Domain.Entities.Base;

namespace Microservices.Domain.Core.Repositories.Interfaces.Generic
{
    public interface IRepository<TEntity, TKey> : IDisposable where TEntity : class, IEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(TKey id);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TKey id);
    }
}