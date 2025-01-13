using Catalog.Application.Services.Interfaces;
using Catalog.Domain.Entities;
using Microservices.Domain.Core.Repositories.Interfaces;
using Microservices.Infrastructure.UnitOfWork.Interface;
using MongoDB.Bson;

namespace Catalog.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
            _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                return await _unitOfWork.GetRepository<Product>().GetAll();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetProduct(string id)
        {
            try
            {
                return await _unitOfWork.GetRepository<Product>().GetById(id);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetByCategory(string categoryName)
        {
            try
            {
                return await _productRepository.GetByCategory(categoryName);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            try
            {
                return await _productRepository.GetByName(name);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(Product product)
        {
            try
            {
                if (string.IsNullOrEmpty(product.Id))
                {
                    product.Id = ObjectId.GenerateNewId().ToString();
                }

                _unitOfWork.GetRepository<Product>().Create(product);
                return await _unitOfWork.Commit();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                _unitOfWork.GetRepository<Product>().Delete(id);
                return await _unitOfWork.Commit();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(Product product)
        {
            try
            {
                _unitOfWork.GetRepository<Product>().Update(product);
                return await _unitOfWork.Commit();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}