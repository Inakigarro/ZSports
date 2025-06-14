using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZSports.Establecimientos.Domain;

namespace ZSports.Establecimientos.Persistence.Configurations;

public class CanchaConfiguration : IEntityTypeConfiguration<Cancha>
{
    public void Configure(EntityTypeBuilder<Cancha> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Numero)
            .IsRequired();
        builder.Property(c => c.TipoSuelo)
            .IsRequired();
        builder.Property(c => c.EstablecimientoId)
            .IsRequired();
        builder.HasOne(c => c.Establecimiento)
            .WithMany(e => e.Canchas)
            .HasForeignKey(c => c.EstablecimientoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
