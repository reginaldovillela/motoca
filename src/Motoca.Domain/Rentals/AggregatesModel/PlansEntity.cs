using Motoca.Domain.SeedWork;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Rentals.AggregatesModel;

public class PlansEntity
    : Entity, IAggregateRoot
{
    public string Name { get; set; }

    public ushort Duration { get; set; }

    public double ValuePerDay { get; set; }
}
