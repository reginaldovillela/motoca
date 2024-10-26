using Motoca.Domain.Rentals.AggregatesModel;

namespace Motoca.Infrastructure.Rentals.Mappings;

public class RentalEntityMapping : IEntityTypeConfiguration<RentalEntity>
{
    public void Configure(EntityTypeBuilder<RentalEntity> builder)
    {
        builder.ToTable("rentals");

        builder.Ignore(r => r.DomainEvents);

        builder.HasKey(r => r.EntityId);

        builder.Ignore(r=> r.ExpectedEndDate);

        //builder.Ignore(r=> r.AmountToPay);

        builder.Ignore(r=> r.IsActive);

        builder.Ignore(r=> r.IsOverDue);

        builder.Ignore(r=> r.DaysOverDue);

        builder.HasIndex(r => r.Id)
            .IsUnique();

        builder.HasOne(r=> r.Plan)
            .WithMany(r => r.Rentals)
            .HasForeignKey(r=> r.PlanEntityId)
            .IsRequired();

        builder.Property(r => r.EntityId)
            .HasColumnName("InternalId");

        builder.Property(r => r.Id)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(r => r.CreateAt)
            .IsRequired();

        builder.Property(r => r.StartDate)
            .IsRequired();
    }
}

public static class RentalEntityMappingExtensions
{
    public static void ApplyRentalEntityMapping(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RentalEntityMapping());
    }
}