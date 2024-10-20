using Motoca.Domain.Bikes.Exceptions;
using Motoca.Domain.SeedWork;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Bikes.AggregatesModel;

public class BikeEntity
    : Entity, IAggregateRoot
{
    public string Id { get; init; }

    public ushort Year { get; private set; } = 0;

    public string Model { get; private set; } = string.Empty;

    public string LicensePlate { get; private set; } = string.Empty;

    // ef required
#pragma warning disable CS8618
    protected BikeEntity() { }
#pragma warning restore CS8618

    public BikeEntity(string id)
    {
        Id = id;
    }

    public void SetYear(ushort year)
    {
        if (year < 2000)
            throw new BikesDomainException(EntityId, Id, "Essa motoca é muito velhinha");

        if (year > DateTime.UtcNow.Year + 1)
            throw new BikesDomainException(EntityId, Id, "Essa motoca é nova demais. Ela vem do futuro?!");

        Year = year;
    }

    public void SetModel(string model)
    {
        Model = model;
    }

    public void SetLicensePlate(string licensePlate)
    {
        LicensePlate = licensePlate;
    }
}
