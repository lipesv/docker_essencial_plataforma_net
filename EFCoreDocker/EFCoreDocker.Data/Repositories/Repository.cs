using Microsoft.EntityFrameworkCore;
using EFCoreDocker.Data.Repositories.Interfaces;

namespace EFCoreDocker.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _set;

        public Repository(DbContext context)
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _set.ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            await _set.AddAsync(entity);
        }

        public async Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var entity = await _set.FindAsync(id);
            _set.Remove(entity);
        }
    }
}