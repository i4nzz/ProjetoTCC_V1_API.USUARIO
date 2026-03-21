using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Data;

public class FilhosContext : DbContext
{
    public FilhosContext(DbContextOptions<FilhosContext> options) : base(options) { }

    public DbSet<Filho> Filhos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FilhosContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
