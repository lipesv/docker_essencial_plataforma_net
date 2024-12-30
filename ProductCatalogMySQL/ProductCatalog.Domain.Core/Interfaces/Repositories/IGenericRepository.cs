using System.Linq.Expressions;
using ProductCatalog.Domain.Entities.Base;

namespace ProductCatalog.Domain.Core.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task AddAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<bool> Exist(Expression<Func<TEntity, bool>> predicate);
    }
}
