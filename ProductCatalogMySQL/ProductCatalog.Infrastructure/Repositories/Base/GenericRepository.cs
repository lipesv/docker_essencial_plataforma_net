using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ProductCatalog.Domain.Core.Interfaces.Repositories;
using ProductCatalog.Domain.Entities.Base;
using ProductCatalog.Infrastructure.Context;

namespace ProductCatalog.Infrastructure.Repositories.Base
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TEntity> _entitiySet;


        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _entitiySet = _dbContext.Set<TEntity>();
        }


        public void Add(TEntity entity)
            => _dbContext.Add(entity);


        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
            => await _dbContext.AddAsync(entity, cancellationToken);


        public void AddRange(IEnumerable<TEntity> entities)
            => _dbContext.AddRange(entities);


        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => await _dbContext.AddRangeAsync(entities, cancellationToken);

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
            => _entitiySet.FirstOrDefault(expression);


        public IEnumerable<TEntity> GetAll()
            => _entitiySet.AsEnumerable();


        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression)
            => _entitiySet.Where(expression).AsEnumerable();

        public async Task<TEntity> GetAsync<TKey>(TKey id)
        {
            return await _entitiySet.FindAsync(id);
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _entitiySet.ToListAsync(cancellationToken);


        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression,
                                                            CancellationToken cancellationToken = default)
            => await _entitiySet.Where(expression).ToListAsync(cancellationToken);


        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression,
                                            CancellationToken cancellationToken = default)
            => await _entitiySet.FirstOrDefaultAsync(expression, cancellationToken);


        public void Remove(TEntity entity)
            => _dbContext.Remove(entity);


        public void RemoveRange(IEnumerable<TEntity> entities)
            => _dbContext.RemoveRange(entities);


        public void Update(TEntity entity)
            => _dbContext.Update(entity);


        public void UpdateRange(IEnumerable<TEntity> entities)
            => _dbContext.UpdateRange(entities);

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
            => await _entitiySet.AnyAsync(predicate);


    }
}