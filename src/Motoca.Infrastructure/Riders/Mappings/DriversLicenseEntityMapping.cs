using Motoca.Domain.Riders.AggregatesModel;

namespace Motoca.Infrastructure.Riders.Mappings;

public class DriversLicenseEntityMapping : IEntityTypeConfiguration<DriversLicenseEntity>
{
    public void Configure(EntityTypeBuilder<DriversLicenseEntity> builder)
    {
        builder.ToTable("drivers_licenses");

        builder.Ignore(d => d.DomainEvents);

        builder.HasKey(d => d.EntityId);

        builder.HasOne(d => d.Rider)
            .WithOne(d => d.DriversLicense)
            .HasForeignKey<DriversLicenseEntity>(d => d.RiderEntityId)
            .IsRequired();

        builder.Property(d => d.EntityId)
            .HasColumnName("InternalId");

        builder.Property(d => d.RiderEntityId)
            .IsRequired()
            .HasColumnName("RiderId")
            .HasMaxLength(50);

        builder.Property(d => d.Number)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Category)
            .IsRequired();
    }
}

public static class DriversLicenseEntityMappingExtensions
{
    public static void ApplyDriversLicenseEntityMapping(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DriversLicenseEntityMapping());
    }
}
