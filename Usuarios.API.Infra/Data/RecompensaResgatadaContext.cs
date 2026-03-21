using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;
using Usuarios.API.Infrastructure.Data;

namespace Usuarios.API.Infra.Data;

public class RecompensaResgatadaContext : DbContext
{
    public RecompensaResgatadaContext(DbContextOptions<RecompensaResgatadaContext> options) :base(options) {}
    public DbSet<RecompensaResgatada> RecompensasResgatadas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecompensaResgatadaContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
