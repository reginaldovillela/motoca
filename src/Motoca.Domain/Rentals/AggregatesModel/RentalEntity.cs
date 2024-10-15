using Motoca.Domain.SeedWork;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Rentals.AggregatesModel;

public class RentalEntity
    : Entity, IAggregateRoot
{
    public double ValorPerDay { get; private set; }

    public string RiderId { get; private set; }

    public string BikeId { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public DateTime ExpectedEndDate { get; private set; }

    public DateTime? ReturnDate { get; private set; }
}
