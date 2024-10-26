using Motoca.Domain.Riders.AggregatesModel;

namespace Motoca.Infrastructure.Riders.Mappings;

public class RiderEntityMapping : IEntityTypeConfiguration<RiderEntity>
{
    public void Configure(EntityTypeBuilder<RiderEntity> builder)
    {
        builder.ToTable("riders");

        builder.Ignore(r => r.DomainEvents);

        builder.HasKey(r => r.EntityId);

        builder.HasIndex(r => r.Id)
            .IsUnique();

        builder.HasOne(r => r.DriversLicense)
            .WithOne(r => r.Rider)
            .HasForeignKey<DriversLicenseEntity>(d => d.RiderEntityId)
            .IsRequired();

        builder.Property(r => r.EntityId)
            .HasColumnName("InternalId");

        builder.Property(r => r.Id)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne(r => r.SocialId)
            .Property(r => r.Number)
            .IsRequired()
            .HasColumnName("SocialId")
            .HasMaxLength(20);

        builder.Property(r => r.BirthDate)
            .IsRequired()
            .HasColumnType("date");
    }
}

public static class RiderEntityMappingExtensions
{
    public static void ApplyRiderEntityMapping(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RiderEntityMapping());

        var birthDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-20));

        var rider = new RiderEntity("entregador-exemplo",
                                    "Fulano de Tal",
                                    birthDate);

        modelBuilder.Entity<RiderEntity>().HasData(rider);
        modelBuilder
            .Entity<DriversLicenseEntity>()
            .HasData(new DriversLicenseEntity("1234567890", "A")
            {
                RiderEntityId = rider.EntityId,
            });
        modelBuilder
            .Entity<RiderEntity>()
            .OwnsOne(p => p.SocialId)
            .HasData(new SocialIdVO("12345678900")
            {
                RiderEntityId = rider.EntityId
            });
    }
}