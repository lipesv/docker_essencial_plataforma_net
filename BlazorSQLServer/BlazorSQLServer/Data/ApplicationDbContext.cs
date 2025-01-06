using BlazorSQLServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace BlazorSQLServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

            if (dbCreator != null)
            {
                // Create Database 
                if (!dbCreator.CanConnect())
                {
                    dbCreator.Create();
                }

                // Create Tables
                if (!dbCreator.HasTables())
                {
                    dbCreator.CreateTables();
                }
            }
        }

        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contato>().HasData(
                new Contato() { Id = 1, Nome = "Isabela Isis Giovanna da Cunha", Email = "isabela_isis_dacunha@balloons.com.br" },
                new Contato() { Id = 2, Nome = "Daniel Isaac Ryan Drumond", Email = "daniel-drumond91@monetto.com.br" },
                new Contato() { Id = 3, Nome = "Henry Anthony Aparício", Email = "henry_anthony_aparicio@zipmail.com.br" });

            base.OnModelCreating(modelBuilder);
        }
    }
}
