using ProductCatalog.Domain.Entities.Base;

namespace ProductCatalog.Domain.Core.Interfaces.Repositories
{
    public interface IRepositoryFactory
    {
        IGenericRepository<T> GetRepository<T>() where T : BaseEntity;
    }
}
