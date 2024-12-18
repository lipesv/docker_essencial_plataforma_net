using Microsoft.EntityFrameworkCore;
using EFCoreDocker.Data.Repositories.Interfaces;
using EFCoreDocker.Domain.Entities.Base;

namespace EFCoreDocker.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
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
            try
            {
                await _set.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                var currentEntity = await _set.SingleOrDefaultAsync(e => e.Id.Equals(entity.Id));

                if (currentEntity == null)
                    return null;

                _set.Entry(currentEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _set.FindAsync(id);

                if (entity == null)
                    return false;

                _set.Remove(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}