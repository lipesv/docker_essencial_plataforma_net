using Domain.Core.Entities;

namespace Domain.Core.Repositories.Generic
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(object id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
    }
}