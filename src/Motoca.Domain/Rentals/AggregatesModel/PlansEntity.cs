using Motoca.Domain.SeedWork;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Rentals.AggregatesModel;

public class PlansEntity
    : Entity, IAggregateRoot
{
    //public Guid RentalEntityId { get; init; }

    public string Id { get; init; }

    public ushort DefaultDuration { get; init; }

    public double ValuePerDay { get; init; }

    //public virtual RentalEntity Rental { get; init; }

        // ef required
#pragma warning disable CS8618
    protected PlansEntity() { }
#pragma warning restore CS8618

    public PlansEntity(string id, ushort defaultDuration, double valuePerDay)
    {
        Id = id;
        DefaultDuration = defaultDuration;
        ValuePerDay = valuePerDay;
    }
}
