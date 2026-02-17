using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Mappings
{
    public class ComprovacaoTarefaMap : IEntityTypeConfiguration<ComprovacaoTarefa>
    {
        public void Configure(EntityTypeBuilder<ComprovacaoTarefa> builder)
        {
            builder.ToTable("comprovacao_tarefa");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UrlFoto)
                   .IsRequired()
                   .HasMaxLength(300);

            builder.Property(x => x.DataEnvio)
                   .IsRequired();

            builder.Property(x => x.Validada)
                   .IsRequired();

            builder.HasOne(x => x.Tarefa)
                   .WithMany()
                   .HasForeignKey(x => x.TarefaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
