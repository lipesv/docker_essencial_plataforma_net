using Catalog.Application.Services.Interfaces;
using Catalog.Domain.Entities;
using Microservices.Domain.Core.Repositories.Interfaces;
using Microservices.Domain.Core.Repositories.Interfaces.Generic;
using Microservices.Infrastructure.UnitOfWork.Interface;
using MongoDB.Bson;

namespace Catalog.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product, string> _repository;
        private readonly IProductRepository _productRepository;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
            _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));

            _repository = _unitOfWork.GetRepository<Product, string>();

        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                return await _repository.GetAll();
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
                return await _repository.GetById(id);
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

                _repository.Add(product);
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
                _repository.Update(product);
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
                _repository.Delete(id);
                return await _unitOfWork.Commit();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}