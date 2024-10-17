using Motoca.Domain.Bikes.AggregatesModel;

namespace Motoca.Infrastructure.Bikes.Mapppings;

public class BikeEntityMapping : IEntityTypeConfiguration<BikeEntity>
{
    public void Configure(EntityTypeBuilder<BikeEntity> builder)
    {
        builder.ToTable("bikes");

        builder.Ignore(b=> b.DomainEvents);

    }
}
