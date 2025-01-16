namespace Infrastructure.Context.Interfaces
{
    public interface IStorageContext : IDisposable
    {
        Task<int> SaveChanges();
    }
}