using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Data;

public class MesadaContext : DbContext
{
    public MesadaContext(DbContextOptions<MesadaContext> options) : base(options) { }
    public DbSet<Mesada> Mesadas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MesadaContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
