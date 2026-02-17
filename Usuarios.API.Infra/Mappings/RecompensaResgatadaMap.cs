using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Mappings
{
    public class RecompensaResgatadaMap : IEntityTypeConfiguration<RecompensaResgatada>
    {
        public void Configure(EntityTypeBuilder<RecompensaResgatada> builder)
        {
            builder.ToTable("recompensa_resgatada");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DataResgate)
                   .IsRequired();

            builder.HasOne(x => x.Filho)
                   .WithMany()
                   .HasForeignKey(x => x.FilhoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Recompensa)
                   .WithMany()
                   .HasForeignKey(x => x.RecompensaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
