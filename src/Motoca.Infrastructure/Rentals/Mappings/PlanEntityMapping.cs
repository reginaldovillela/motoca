using Motoca.Domain.Rentals.AggregatesModel;

namespace Motoca.Infrastructure.Rentals.Mappings;

public class PlanEntityMapping : IEntityTypeConfiguration<PlanEntity>
{
    public void Configure(EntityTypeBuilder<PlanEntity> builder)
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

public static class PlanEntityMappingExtensions
{
    public static void ApplyPlanEntityMapping(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PlanEntityMapping());

        modelBuilder.Entity<PlanEntity>().HasData(
            new PlanEntity("plano7", 7, 30),
            new PlanEntity("plano15", 15, 28),
            new PlanEntity("plano30", 30, 22),
            new PlanEntity("plano45", 45, 20),
            new PlanEntity("plano50", 50, 18)
        );
    }
}