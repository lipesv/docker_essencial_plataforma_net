namespace Product.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Domain.Entities.Product>> GetProductsAsync();
        Task<Domain.Entities.Product> GetProductAsync(string id);
        Task<IEnumerable<Domain.Entities.Product>> GetByNameAsync(string name);
        Task<IEnumerable<Domain.Entities.Product>> GetByCategoryAsync(string categoryName);
        Task Create(Domain.Entities.Product product);
        Task<bool> Update(Domain.Entities.Product product);
        Task<bool> Delete(string id);
    }
}