using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Mappings
{
    public class CategoriaFinanceiraMap : IEntityTypeConfiguration<CategoriaFinanceira>
    {
        public void Configure(EntityTypeBuilder<CategoriaFinanceira> builder)
        {
            builder.ToTable("categoria_financeira");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }

}
