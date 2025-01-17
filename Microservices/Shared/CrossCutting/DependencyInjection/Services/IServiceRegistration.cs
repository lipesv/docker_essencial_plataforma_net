using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection.Services
{
    public interface IServiceRegistration
    {
        void RegisterServices(IServiceCollection services, IConfiguration configuration);
    }
}