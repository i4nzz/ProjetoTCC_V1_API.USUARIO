using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestaoTarefas.Domain.Entities;

namespace GestaoTarefas.Infra.Mappings
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
                   .WithMany(t => t.Comprovacoes)
                   .HasForeignKey(x => x.TarefaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
