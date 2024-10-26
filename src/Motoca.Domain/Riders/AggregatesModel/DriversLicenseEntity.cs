using Motoca.Domain.SeedWork;

namespace Motoca.Domain.Riders.AggregatesModel;

public class DriversLicenseEntity
    : Entity
{
    public Guid RiderEntityId { get; init; }

    public string Number { get; init; }

    public string Category { get; init; }

    public RiderEntity Rider { get; init; } = null!;

    // ef required
#pragma warning disable CS8618
    protected DriversLicenseEntity() { }
#pragma warning restore CS8618

    public DriversLicenseEntity(string number, string category)
    {
        Number = number;
        Category = category;
    }
}
