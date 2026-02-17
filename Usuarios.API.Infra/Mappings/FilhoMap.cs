using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Mappings
{
    public class FilhoMap : IEntityTypeConfiguration<Filho>
    {
        public void Configure(EntityTypeBuilder<Filho> builder)
        {
            builder.ToTable("filho");

            builder.Property(x => x.DataNascimento)
                   .IsRequired();
        }
    }

}
