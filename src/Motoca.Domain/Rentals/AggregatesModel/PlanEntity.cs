using Motoca.Domain.SeedWork;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Rentals.AggregatesModel;

public class PlanEntity
    : Entity, IAggregateRoot
{
    public string Id { get; init; }

    public ushort DefaultDuration { get; init; }

    public double ValuePerDay { get; init; }

    public ICollection<RentalEntity> Rentals { get; } = [];

    // ef required
#pragma warning disable CS8618
    protected PlanEntity() { }
#pragma warning restore CS8618

    public PlanEntity(string id, ushort defaultDuration, double valuePerDay)
    {
        Id = id;
        DefaultDuration = defaultDuration;
        ValuePerDay = valuePerDay;
    }
}
