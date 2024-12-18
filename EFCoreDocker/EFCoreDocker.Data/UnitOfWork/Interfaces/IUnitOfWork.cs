using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDocker.Domain.Entities.Base;

namespace EFCoreDocker.Data.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
        void Rollback();
        IRepository<T> Repository<T>() where T : BaseEntity;
    }
}