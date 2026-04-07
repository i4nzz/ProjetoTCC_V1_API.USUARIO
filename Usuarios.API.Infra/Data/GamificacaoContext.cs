using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Data
{
    public class GamificacaoContext : DbContext
    {
        public GamificacaoContext(DbContextOptions<GamificacaoContext> options): base(options) { }

        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<ComprovacaoTarefa> ComprovacoesTarefa { get; set; }
        public DbSet<Pontuacao> Pontuacoes { get; set; }
        public DbSet<Recompensa> Recompensas { get; set; }
        public DbSet<RecompensaResgatada> RecompensasResgatadas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GamificacaoContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }

}
