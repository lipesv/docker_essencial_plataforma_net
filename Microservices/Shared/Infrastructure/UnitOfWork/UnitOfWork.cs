using System.Collections.Concurrent;
using Common.Domain.Core.Entities;
using Common.Domain.Core.Repositories.Generic;
using Common.Infrastructure.Context.Interfaces;
using Common.Infrastructure.UnitOfWork.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConcurrentDictionary<Type, object> _repositories;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _repositories = new ConcurrentDictionary<Type, object>(); // Initialize _repositories
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {
            return (IRepository<TEntity>)_repositories.GetOrAdd(typeof(TEntity), _ =>
            {
                // Use DI to resolve the repository
                var repository = _serviceProvider.GetService<IRepository<TEntity>>();
                if (repository == null)
                {
                    throw new InvalidOperationException($"No registered repository for {typeof(TEntity).Name}");
                }
                return repository;
            });
        }
        public async Task<bool> Commit()
        {
            var context = _serviceProvider.GetRequiredService<IStorageContext>();
            var changeAmount = await context.SaveChanges();

            return changeAmount > 0;
        }
    }
}
