using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Data;

public class PontuacaoContext : DbContext
{
    public PontuacaoContext(DbContextOptions<PontuacaoContext> options) { }
    public DbSet<Pontuacao> Pontuacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PontuacaoContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
