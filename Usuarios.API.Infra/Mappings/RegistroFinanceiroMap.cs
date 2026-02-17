using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Mappings
{
    public class RegistroFinanceiroMap : IEntityTypeConfiguration<RegistroFinanceiro>
    {
        public void Configure(EntityTypeBuilder<RegistroFinanceiro> builder)
        {
            builder.ToTable("registro_financeiro");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
                   .HasMaxLength(300);

            builder.Property(x => x.Valor)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.Property(x => x.DataRegistro)
                   .IsRequired();

            builder.HasOne(x => x.Filho)
                   .WithMany()
                   .HasForeignKey(x => x.FilhoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Categoria)
                   .WithMany()
                   .HasForeignKey(x => x.CategoriaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Mesada)
                   .WithMany()
                   .HasForeignKey(x => x.MesadaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
