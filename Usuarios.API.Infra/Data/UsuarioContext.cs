using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infrastructure.Data;

public class UsuarioContext : DbContext
{
    public UsuarioContext(DbContextOptions<UsuarioContext> options)
        : base(options)
    {
    }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Pai> Pais { get; set; }
    public DbSet<Filho> Filhos { get; set; }
    public DbSet<PaisFilhos> PaisFilhos { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<ComprovacaoTarefa> ComprovacoesTarefa { get; set; }
    public DbSet<Pontuacao> Pontuacoes { get; set; }
    public DbSet<Recompensa> Recompensas { get; set; }
    public DbSet<RecompensaResgatada> RecompensasResgatadas { get; set; }
    public DbSet<Mesada> Mesadas { get; set; }
    public DbSet<CategoriaFinanceira> CategoriasFinanceiras { get; set; }
    public DbSet<RegistroFinanceiro> RegistrosFinanceiros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsuarioContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
