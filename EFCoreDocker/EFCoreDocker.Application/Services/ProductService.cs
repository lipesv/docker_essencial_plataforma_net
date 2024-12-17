using EFCoreDocker.Application.Services.Interfaces;
using EFCoreDocker.Data.Repositories.Interfaces;
using EFCoreDocker.Domain.Entities;

namespace EFCoreDocker.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                return await _unitOfWork.Repository<Product>().GetAll();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetById(int id)
        {
            try
            {
                return await _unitOfWork.Repository<Product>().GetById(id);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task Create(Product product)
        {
            try
            {
                await _unitOfWork.Repository<Product>().Add(product);
                await _unitOfWork.Commit();
            }
            catch (System.Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task Update(Product product)
        {
            try
            {
                await _unitOfWork.Repository<Product>().Update(product);
                await _unitOfWork.Commit();
            }
            catch (System.Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await _unitOfWork.Repository<Product>().Delete(id);
                await _unitOfWork.Commit();
            }
            catch (System.Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}