using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Data
{
    public class FinanceiroContext : DbContext
    {
        public DbSet<CategoriaFinanceira> CategoriasFinanceiras { get; set; }
        public DbSet<RegistroFinanceiro> RegistrosFinanceiros { get; set; }
        public DbSet<Mesada> Mesadas { get; set; }
    }
}
