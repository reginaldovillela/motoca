using Motoca.Domain.SeedWork;

namespace Motoca.Domain.Riders.AggregatesModel;

public class DriversLicenseEntity
    : Entity
{
    public string RiderId { get; init; }

    public string Number { get; init; }

    public string Category { get; init; }

    public string Base64Image { get; init; }
}
