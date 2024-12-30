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
            return await _unitOfWork.GetRepository<Product>().GetAllAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _unitOfWork.GetRepository<Product>().GetByIdAsync(id);
        }

        public async Task Create(Product product)
        {
            await _unitOfWork.GetRepository<Product>().AddAsync(product);
            await _unitOfWork.CommitAsync();
        }

        public async Task Create(IEnumerable<Product> products)
        {
            await _unitOfWork.GetRepository<Product>().AddAsync(products);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> Update(Product product)
        {
            if (await _unitOfWork.GetRepository<Product>().Exist(p => p.Id == product.Id))
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
            var entityToDelete = await _unitOfWork.GetRepository<Product>().GetByIdAsync(id);

            if (entityToDelete != null)
            {
                _unitOfWork.GetRepository<Product>().Delete(entityToDelete);
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