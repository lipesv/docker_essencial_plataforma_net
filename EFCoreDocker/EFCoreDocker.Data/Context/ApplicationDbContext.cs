using EFCoreDocker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDocker.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
    }
}