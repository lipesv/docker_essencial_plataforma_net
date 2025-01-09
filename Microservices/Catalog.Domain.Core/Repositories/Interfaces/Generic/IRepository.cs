using Catalog.Domain.Entities.Base;

namespace Catalog.Domain.Core.Repositories.Interfaces.Generic
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        void Create(TEntity obj);
        void Update(TEntity obj);
        void Delete(string id);
        Task<TEntity> GetById(string id);
        Task<IEnumerable<TEntity>> GetAll();
    }
}