using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Data
{
    public class PaisFilhosContext : DbContext
    {
        public PaisFilhosContext(DbContextOptions<PaisFilhosContext> options): base(options) {}
        public DbSet<PaisFilhos> PaisFilhos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaisFilhosContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
