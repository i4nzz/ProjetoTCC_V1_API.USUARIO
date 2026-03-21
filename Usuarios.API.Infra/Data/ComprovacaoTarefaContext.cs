using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;
using Usuarios.API.Infrastructure.Data;

namespace Usuarios.API.Infra.Data;

public class ComprovacaoTarefaContext : DbContext
{
    public ComprovacaoTarefaContext(DbContextOptions<ComprovacaoTarefaContext> options) : base(options) { }

    public DbSet<ComprovacaoTarefa> ComprovacoesTarefa { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComprovacaoTarefaContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}