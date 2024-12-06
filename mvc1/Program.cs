using Microsoft.EntityFrameworkCore;
using mvc1.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        IConfiguration configuration = builder.Configuration;

        builder.Services.AddControllersWithViews();

        builder.Services.AddSingleton<IConfiguration>(configuration);
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddTransient<IRepository, ProdutoRepository>();

        var host = Environment.GetEnvironmentVariable("MYSQL_HOST");
        var pass = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");

        var connStr = $"Server={host};Database=produtosdb;User ID=root;Password={pass};";

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connStr, ServerVersion.AutoDetect(connStr))
        );

        var app = builder.Build();

        Populadb.IncluiDadosDB(app);

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}