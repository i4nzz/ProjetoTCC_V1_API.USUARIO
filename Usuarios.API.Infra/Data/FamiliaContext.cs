using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Data
{
    public class FamiliaContext : DbContext
    {
        public DbSet<Pai> Pais { get; set; }
        public DbSet<Filho> Filhos { get; set; }
        public DbSet<PaisFilhos> PaisFilhos { get; set; }
    }
}
