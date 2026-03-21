using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Data;

public class PaiContext : DbContext
{
    public PaiContext(DbContextOptions<PaiContext> options): base(options)
    {
    }
    public DbSet<Pai> Pais { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaiContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
