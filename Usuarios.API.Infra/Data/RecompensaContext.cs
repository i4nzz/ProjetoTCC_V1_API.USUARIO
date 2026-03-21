using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Data;

public class RecompensaContext : DbContext
{
    public RecompensaContext(DbContextOptions<RecompensaContext> options) : base(options) { }
    public DbSet<Recompensa> Recompensas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecompensaContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
