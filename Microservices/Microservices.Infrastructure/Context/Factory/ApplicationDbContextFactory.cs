namespace Microservices.Infrastructure.Factory
{
    // public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    // {
    //     public ApplicationDbContext CreateDbContext(string[] args)
    //     {
    //         IConfigurationRoot configuration = new ConfigurationBuilder()
    //             .SetBasePath(Directory.GetCurrentDirectory())
    //             .AddJsonFile("appsettings.json")
    //             .Build();

    //         // Criando o DbContextOptionsBuilder manualmente
    //         var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

    //         // cria a connection string. 
    //         // requer a connectionstring no appsettings.json
    //         var connectionString = configuration.GetConnectionString("DefaultConnection");

    //         builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

    //         // Cria o contexto
    //         return new ApplicationDbContext(builder.Options);
    //     }
    // }
}
