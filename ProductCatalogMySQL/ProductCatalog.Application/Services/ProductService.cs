using ProductCatalog.Application.Services.Interfaces;
using ProductCatalog.Domain.Core.Interfaces.UnitOfWork;
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
                return await _unitOfWork.GetRepository<Product>().GetAllAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetById(int id)
        {
            return await _unitOfWork.GetRepository<Product>().GetAsync(id);
        }

        public async Task Create(Product product)
        {
            _unitOfWork.GetRepository<Product>().Add(product);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> Update(Product product)
        {
            if (await _unitOfWork.GetRepository<Product>().Exists(p => p.Id == product.Id))
            {
                _unitOfWork.GetRepository<Product>().Update(product);
                await _unitOfWork.CommitAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var entityToDelete = await _unitOfWork.GetRepository<Product>().GetAsync(id);

            if (entityToDelete != null)
            {
                _unitOfWork.GetRepository<Product>().Remove(entityToDelete);
                await _unitOfWork.CommitAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}