using CrossCutting.DependencyInjection;
using CrossCutting.DependencyInjection.Repository;
using CrossCutting.DependencyInjection.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new ServiceCollectionBuilder(services)
                 .AddConfiguration(configuration);

            // Register all IServiceRegistration implementations
            var serviceRegistrations = GetRegistrations<IServiceRegistration>();
            foreach (var registration in serviceRegistrations)
            {
                builder.RegisterService(registration, configuration);
            }

            // Register all IRepositoryRegistration implementations
            var repositoryRegistrations = GetRegistrations<IRepositoryRegistration>();
            foreach (var registration in repositoryRegistrations)
            {
                builder.RegisterRepository(registration, configuration);
            }

            builder.Build();
        }

        private static T[] GetRegistrations<T>() where T : class
        {
            // Dynamically load all implementations of the given interface (T) from the current AppDomain
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(t => Activator.CreateInstance(t) as T)
                .ToArray();
        }
    }
}