using System.Collections.Concurrent;
using Microservices.Domain.Core.Entities;
using Microservices.Domain.Core.Repositories.Generic;
using Microservices.Infrastructure.Context.Interfaces;
using Microservices.Infrastructure.Repositories.Base;
using Microservices.Infrastructure.UnitOfWork.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Infrastructure.UnitOfWork
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

        // public void Dispose()
        // {
        //     if (_disposed)
        //         return;

        //     // Dispose of all services that need to be disposed of, such as MongoContext
        //     var context = _serviceProvider.GetService<IStorageContext>();
        //     context?.Dispose();

        //     _disposed = true;
        //     GC.SuppressFinalize(this);
        // }
    }
}
