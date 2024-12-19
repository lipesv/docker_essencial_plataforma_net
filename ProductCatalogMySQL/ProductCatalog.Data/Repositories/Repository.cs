using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.Repositories.Interfaces;
using ProductCatalog.Domain.Entities.Base;

namespace ProductCatalog.Data.Repositories
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
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
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
                
                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}