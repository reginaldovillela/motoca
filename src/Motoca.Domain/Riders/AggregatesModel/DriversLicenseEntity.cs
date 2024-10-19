using Motoca.Domain.SeedWork;

namespace Motoca.Domain.Riders.AggregatesModel;

public class DriversLicenseEntity
    : Entity
{
    public Guid RiderEntityId { get; init; }

    public string Number { get; init; }

    public string Category { get; init; }

    //public string Base64Image { get; init; }

    public virtual RiderEntity Rider { get; init; }

    // ef required
#pragma warning disable CS8618
    protected DriversLicenseEntity() { }
#pragma warning restore CS8618

    public DriversLicenseEntity(string number, string category)
    {
        Number = number;
        Category = category;
        //Base64Image = base64Image;
    }
}
