using Motoca.Domain.Rentals.AggregatesModel;

namespace Motoca.Infrastructure.Rentals.Mappings;

public class RentalsEntityMapping : IEntityTypeConfiguration<RentalEntity>
{
    public void Configure(EntityTypeBuilder<RentalEntity> builder)
    {
        builder.ToTable("rentals");

        builder.Ignore(r => r.DomainEvents);

        builder.HasKey(r => r.EntityId);

        builder.Ignore(r=> r.ExpectedEndDate);

        builder.Ignore(r=> r.AmountToPay);

        builder.HasIndex(r => r.Id)
            .IsUnique();


        builder.Property(r => r.EntityId)
            .HasColumnName("InternalId");

        builder.Property(r => r.Id)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(r => r.CreateAt)
            .IsRequired();

        builder.Property(r => r.StartDate)
            .IsRequired()
            .HasColumnType("date");
    }
}
