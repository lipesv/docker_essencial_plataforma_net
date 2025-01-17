using Common.CrossCutting.DependencyInjection.Repository;
using Common.CrossCutting.DependencyInjection.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.CrossCutting.DependencyInjection
{
    public class ServiceCollectionBuilder
    {
        private readonly IServiceCollection _services;

        public ServiceCollectionBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public ServiceCollectionBuilder AddConfiguration(IConfiguration configuration)
        {
            _services.AddSingleton(configuration);
            return this;
        }

        // public ServiceCollectionBuilder AddModules(IConfiguration configuration)
        // {
        //     var serviceRegistrations = Assembly.GetExecutingAssembly()
        //         .GetTypes()
        //         .Where(type => typeof(IServiceRegistration).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
        //         .Select(Activator.CreateInstance)
        //         .Cast<IServiceRegistration>();

        //     foreach (var registration in serviceRegistrations)
        //     {
        //         registration.RegisterServices(_services, configuration);
        //     }

        //     return this;
        // }

        // public ServiceCollectionBuilder AddRepositoryModules(IConfiguration configuration)
        // {
        //     var repositoryRegistrations = Assembly.GetExecutingAssembly()
        //         .GetTypes()
        //         .Where(type => typeof(IRepositoryRegistration).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
        //         .Select(Activator.CreateInstance)
        //         .Cast<IRepositoryRegistration>();

        //     foreach (var registration in repositoryRegistrations)
        //     {
        //         registration.RegisterRepositories(_services, configuration);
        //     }

        //     return this;
        // }

        public ServiceCollectionBuilder RegisterService(IServiceRegistration registration, IConfiguration configuration)
        {
            registration.RegisterServices(_services, configuration);
            return this;
        }

        public ServiceCollectionBuilder RegisterRepository(IRepositoryRegistration repositoryRegistration, IConfiguration configuration)
        {
            repositoryRegistration.RegisterRepositories(_services, configuration);
            return this;
        }

        public ServiceCollectionBuilder AddExternalModules(params IServiceRegistration[] externalModules)
        {
            foreach (var module in externalModules)
            {
                module.RegisterServices(_services, null);
            }

            return this;
        }

        public IServiceCollection Build()
        {
            return _services;
        }
    }
}