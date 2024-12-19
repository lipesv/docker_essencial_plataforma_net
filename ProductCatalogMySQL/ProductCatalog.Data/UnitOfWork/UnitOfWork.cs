using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.Context;
using ProductCatalog.Data.Repositories;
using ProductCatalog.Data.Repositories.Interfaces;
using ProductCatalog.Data.UnitOfWork.Interfaces;
using ProductCatalog.Domain.Entities.Base;

namespace ProductCatalog.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Rollback()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            return new Repository<T>(_dbContext);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}