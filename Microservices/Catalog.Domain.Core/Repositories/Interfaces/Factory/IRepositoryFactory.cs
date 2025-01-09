using Catalog.Domain.Core.Repositories.Interfaces.Generic;
using Catalog.Domain.Entities.Base;

namespace Catalog.Domain.Core.Repositories.Interfaces.Factory
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }
}
