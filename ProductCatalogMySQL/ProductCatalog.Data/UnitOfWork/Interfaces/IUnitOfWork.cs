using ProductCatalog.Data.Repositories.Interfaces;
using ProductCatalog.Domain.Entities.Base;

namespace ProductCatalog.Data.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
        void Rollback();
        IRepository<T> Repository<T>() where T : BaseEntity;
    }
}