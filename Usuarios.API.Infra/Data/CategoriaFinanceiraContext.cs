using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Data;

public class CategoriaFinanceiraContext : DbContext
{
    public CategoriaFinanceiraContext(DbContextOptions<CategoriaFinanceiraContext> options) : base(options) { }
    public DbSet<CategoriaFinanceira> CategoriasFinanceiras { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoriaFinanceiraContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
