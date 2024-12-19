using ProductCatalog.Application.Services.Interfaces;
using ProductCatalog.Data.UnitOfWork.Interfaces;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Services
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
            return await _unitOfWork.Repository<Product>().GetById(id);
        }

        public async Task Create(Product product)
        {
            await _unitOfWork.Repository<Product>().Add(product);
        }

        public async Task<Product> Update(Product product)
        {
            return await _unitOfWork.Repository<Product>().Update(product);
        }

        public async Task<bool> Delete(int id)
        {
            return await _unitOfWork.Repository<Product>().Delete(id);
        }
    }
}