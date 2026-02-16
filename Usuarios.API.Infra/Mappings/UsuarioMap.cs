using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infrastructure.Mappings;

public class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(x => x.Telefone)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(x => x.DataCriacao)
               .IsRequired();
    }
}

