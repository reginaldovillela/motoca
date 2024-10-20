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

        builder.HasMany(r => r.Rentals)
                   .WithOne(r => r.Plan)
                   .HasForeignKey(r => r.PlanEntityId)
                   .IsRequired();

        builder.Property(r => r.EntityId)
            .HasColumnName("InternalId");

        builder.Property(r => r.Id)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.DefaultDuration)
            .IsRequired();

        builder.Property(r => r.ValuePerDay)
            .IsRequired();
    }
}

public static class PlansEntityMappingExtensions
{
    public static void ApplyPlansEntityMapping(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PlansEntityMapping());

        modelBuilder.Entity<PlansEntity>().HasData(
            new PlansEntity("plano7", 7, 30),
            new PlansEntity("plano15", 15, 28),
            new PlansEntity("plano30", 30, 22),
            new PlansEntity("plano45", 45, 20),
            new PlansEntity("plano50", 50, 18)
        );
    }
}