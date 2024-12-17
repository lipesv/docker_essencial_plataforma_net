using EFCoreDocker.Application.Services;
using EFCoreDocker.Application.Services.Interfaces;
using EFCoreDocker.Data.Context;
using EFCoreDocker.Data.Repositories;
using EFCoreDocker.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreDocker.CrossCutting.Extensions
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
            _services.AddSingleton<IConfiguration>(configuration);
            return this;
        }

        public ServiceCollectionBuilder AddServices()
        {
            _services.AddTransient<IProductService, ProductService>();
            _services.AddScoped<IUnitOfWork, UnitOfWork>();
            _services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return this;
        }

        public ServiceCollectionBuilder AddDataBaseContext(string connectionString)
        {
            _services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            return this;
        }

        public IServiceCollection Build()
        {
            return _services;
        }
    }

    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            new ServiceCollectionBuilder(services)
                .AddConfiguration(configuration)
                .AddServices()
                .AddDataBaseContext(configuration.GetConnectionString("SqlConnection"))
                .Build();
        }
    }
}