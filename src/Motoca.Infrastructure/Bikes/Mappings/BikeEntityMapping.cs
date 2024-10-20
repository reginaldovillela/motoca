using Motoca.Domain.Bikes.AggregatesModel;

namespace Motoca.Infrastructure.Bikes.Mappings;

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

        builder.Property(b => b.EntityId)
            .HasColumnName("InternalId");

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

public static class BikeEntityMappingExtensions
{
    public static void ApplyBikeEntityMapping(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BikeEntityMapping());

        var bike = new BikeEntity("moto-exemplo");
        bike.SetYear(2024);
        bike.SetModel("modelo-moto-exemplo");
        bike.SetLicensePlate("XXX0X00");

        modelBuilder.Entity<BikeEntity>().HasData(bike);
    }
}