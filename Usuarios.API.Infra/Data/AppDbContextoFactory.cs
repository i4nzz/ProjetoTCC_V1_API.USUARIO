using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GestaoTarefas.Infra.Data;

public class AppDbContextoFactory : IDesignTimeDbContextFactory<AppDbContexto>
{
    public AppDbContexto CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../GestaoTarefas");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContexto>();
        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContexto(optionsBuilder.Options);
    }
}