using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;
using Usuarios.API.Infrastructure.Data;

namespace Usuarios.API.Infra.Data;

public class RegistroFinanceiroContext : DbContext
{
    public RegistroFinanceiroContext(DbContextOptions<RegistroFinanceiroContext> options) : base(options) { }
    public DbSet<RegistroFinanceiro> RegistrosFinanceiros { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RegistroFinanceiroContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}
