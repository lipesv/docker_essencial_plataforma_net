using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProductCatalog.Data.Context.Factory
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Criando o DbContextOptionsBuilder manualmente
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // cria a connection string. 
            // requer a connectionstring no appsettings.json
            var connectionString = string.Format(configuration.GetConnectionString("DefaultConnection"),
                                                 args:
                                                 [
                                                     Environment.GetEnvironmentVariable("db_uid"),
                                                     Environment.GetEnvironmentVariable("db_pwd"),
                                                     Environment.GetEnvironmentVariable("db")
                                                 ]);

            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            // Cria o contexto
            return new ApplicationDbContext(builder.Options);
        }
    }
}
