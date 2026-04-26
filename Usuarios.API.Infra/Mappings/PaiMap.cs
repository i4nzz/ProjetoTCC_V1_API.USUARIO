using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Infra.Mappings;

public class PaiMap : IEntityTypeConfiguration<Pai>
{
    public void Configure(EntityTypeBuilder<Pai> builder)
    {
        builder.ToTable("pai");
    }
}