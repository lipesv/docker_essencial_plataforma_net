using Catalog.Application.Services.Interfaces;
using Catalog.Domain.Entities;
using Infrastructure.Repositories.Catalog.Interface;
using Infrastructure.UnitOfWork.Interface;
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
            using var uow = _unitOfWork.GetRepository<Product>();
            return await uow.GetAll();
        }

        public async Task<Product> GetProduct(string id)
        {
            using var uow = _unitOfWork.GetRepository<Product>();
            return await uow.GetById(id);
        }

        public async Task<IEnumerable<Product>> GetByCategory(string categoryName)
        {
            return await _productRepository.GetByCategory(categoryName);
        }

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            return await _productRepository.GetByName(name);
        }

        public async Task<bool> Create(Product product)
        {
            if (product.Id == null)
            {
                product.Id = ObjectId.GenerateNewId().ToString();
            }

            using var uow = _unitOfWork.GetRepository<Product>();
            uow.Add(product);

            return await _unitOfWork.Commit();
        }

        public async Task<bool> Update(Product product)
        {
            using var uow = _unitOfWork.GetRepository<Product>();
            uow.Update(product);

            return await _unitOfWork.Commit();
        }

        public async Task<bool> Delete(string id)
        {
            using var uow = _unitOfWork.GetRepository<Product>();
            uow.Delete(id);

            return await _unitOfWork.Commit();
        }
    }
}