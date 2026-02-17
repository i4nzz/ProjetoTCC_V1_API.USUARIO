using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Mappings
{
    public class PontuacaoMap : IEntityTypeConfiguration<Pontuacao>
    {
        public void Configure(EntityTypeBuilder<Pontuacao> builder)
        {
            builder.ToTable("pontuacao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Pontos)
                   .IsRequired();

            builder.Property(x => x.DataRegistro)
                   .IsRequired();

            builder.HasOne(x => x.Filho)
                   .WithMany()
                   .HasForeignKey(x => x.FilhoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Tarefa)
                   .WithOne()
                   .HasForeignKey<Pontuacao>(x => x.TarefaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
