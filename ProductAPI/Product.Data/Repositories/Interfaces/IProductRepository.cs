namespace Product.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Domain.Entities.Product>> GetProducts();
        Task<Domain.Entities.Product> GetProduct(string id);
        Task<IEnumerable<Domain.Entities.Product>> GetProductByName(string name);
        Task<IEnumerable<Domain.Entities.Product>> GetProductByCategory(string categoryName);

        Task CreateProduct(Domain.Entities.Product product);
        Task<bool> UpdateProduct(Domain.Entities.Product product);
        Task<bool> DeleteProduct(string id);
    }
}