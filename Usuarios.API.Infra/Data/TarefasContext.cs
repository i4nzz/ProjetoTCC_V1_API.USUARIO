using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Data;

public class TarefasContext : DbContext
{
    public TarefasContext(DbContextOptions<TarefasContext> options) : base(options) { }
    public DbSet<Tarefa> Tarefas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TarefasContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}
