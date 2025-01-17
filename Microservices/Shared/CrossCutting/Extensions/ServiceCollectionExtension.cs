using System.Reflection;
using Common.CrossCutting.DependencyInjection;
using Common.CrossCutting.DependencyInjection.Repository;
using Common.CrossCutting.DependencyInjection.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.CrossCutting.Extensions
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
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).ToList();

            // Load assemblies from the application base directory
            var loadedPaths = assemblies.Select(a => a.Location).ToArray();
            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var toLoad = referencedPaths.Where(p => !loadedPaths.Contains(p, StringComparer.OrdinalIgnoreCase)).ToList();

            foreach (var path in toLoad)
            {
                try
                {
                    assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not load assembly from path {path}: {ex.Message}");
                }
            }

            // Find implementations of T
            return assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(t => Activator.CreateInstance(t) as T)
                .ToArray();
        }

    }
}