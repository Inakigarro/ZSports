using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZSports.Establecimientos.Domain;
using ZSports.Establecimientos.Domain.Constants;

namespace ZSports.Establecimientos.Persistence.Configurations;

public class EstablecimientoConfiguration : IEntityTypeConfiguration<Establecimiento>
{
    public void Configure(EntityTypeBuilder<Establecimiento> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Nombre)
            .HasMaxLength(EstablecimientoConstants.MaxNombreLength)
            .IsRequired();
        builder.Property(e => e.Direccion)
            .HasMaxLength(EstablecimientoConstants.MaxDireccionLength)
            .IsRequired();
        builder.Property(e => e.Telefono)
            .HasMaxLength(EstablecimientoConstants.MaxTelefonoLength)
            .IsRequired();
        builder.Property(e => e.Email)
            .HasMaxLength(EstablecimientoConstants.MaxEmailLength)
            .IsRequired();
        builder.HasMany(e => e.Canchas)
            .WithOne(c => c.Establecimiento)
            .HasForeignKey(c => c.EstablecimientoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
