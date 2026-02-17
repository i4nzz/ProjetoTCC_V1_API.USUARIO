using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Mappings
{
    public class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("tarefa");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Titulo)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.Descricao)
                   .HasMaxLength(500);

            builder.Property(x => x.Pontos)
                   .IsRequired();

            builder.Property(x => x.Prazo)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.DataCriacao)
                   .IsRequired();

            builder.HasOne(x => x.Filho)
                   .WithMany()
                   .HasForeignKey(x => x.FilhoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
