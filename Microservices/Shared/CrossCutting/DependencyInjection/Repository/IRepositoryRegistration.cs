using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection.Repository
{
    public interface IRepositoryRegistration
    {
        void RegisterRepositories(IServiceCollection services, IConfiguration configuration);
    }
}