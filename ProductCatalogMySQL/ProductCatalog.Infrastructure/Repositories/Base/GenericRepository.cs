using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Core.Interfaces.Repositories;
using ProductCatalog.Domain.Entities.Base;

namespace ProductCatalog.Infrastructure.Repositories.Base
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);
        public async Task AddAsync(IEnumerable<TEntity> entities) => await _dbSet.AddRangeAsync(entities);
        public void Update(TEntity entity) => _dbSet.Update(entity);
        public void Delete(TEntity entity) => _dbSet.Remove(entity);
        public async Task<bool> Exist(Expression<Func<TEntity, bool>> predicate) => await _dbSet.AnyAsync(predicate);

    }
}