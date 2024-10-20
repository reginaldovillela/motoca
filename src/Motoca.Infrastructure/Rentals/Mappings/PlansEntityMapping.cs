using Motoca.Domain.Rentals.AggregatesModel;

namespace Motoca.Infrastructure.Rentals.Mappings;

public class PlansEntityMapping : IEntityTypeConfiguration<PlansEntity>
{
    public void Configure(EntityTypeBuilder<PlansEntity> builder)
    {
        builder.ToTable("rentals_plans");

        builder.Ignore(r => r.DomainEvents);

        builder.HasKey(r => r.EntityId);

        builder.HasIndex(r => r.Id)
            .IsUnique();

        builder.Property(r => r.Id)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.DefaultDuration)
            .IsRequired();

        builder.Property(r => r.ValuePerDay)
            .IsRequired();
    }
}
