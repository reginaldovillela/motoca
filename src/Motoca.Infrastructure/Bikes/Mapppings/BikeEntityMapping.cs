using Motoca.Domain.Bikes.AggregatesModel;

namespace Motoca.Infrastructure.Bikes.Mapppings;

public class BikeEntityMapping : IEntityTypeConfiguration<BikeEntity>
{
    public void Configure(EntityTypeBuilder<BikeEntity> builder)
    {
        builder.ToTable("bikes");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(b => b.EntityId);

        builder.HasIndex(b => b.Id)
            .IsUnique();

        builder.HasIndex(b => b.LicensePlate)
            .IsUnique();

        builder.Property(b => b.Id)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(b => b.Year)
            .IsRequired()
            .HasColumnType("smallint");

        builder.Property(b => b.Model)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(b => b.LicensePlate)
            .IsRequired()
            .HasMaxLength(7);
    }
}
