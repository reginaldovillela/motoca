using Motoca.Domain.SeedWork;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Rentals.AggregatesModel;

public class RentalEntity
    : Entity, IAggregateRoot
{
    public string Id { get; init; }

    public string RiderId { get; init; }

    public string BikeId { get; init; }

    public Guid PlanEntityId { get; set; }

    public DateTime CreateAt { get; init; }

    public DateOnly StartDate { get; init; }

    public DateOnly ExpectedEndDate => CalculateExpectedEndDate();

    public DateTime? ReturnDate { get; private set; }

    #region "ef relations"

    public PlanEntity Plan { get; set; } = null!;

    #endregion

    public double AmountToPay => CalculateAmountToPay();

    // ef required
#pragma warning disable CS8618
    protected RentalEntity() { }
#pragma warning restore CS8618

    public RentalEntity(string riderId, string bikeId, PlanEntity plan)
    {
        Id = $"{riderId}-{bikeId}";
        RiderId = riderId;
        BikeId = bikeId;
        Plan = plan;
        PlanEntityId = plan.EntityId;

        CreateAt = DateTime.Now;
        StartDate = DateOnly.FromDateTime(CreateAt.AddDays(1));
    }

    private DateOnly CalculateExpectedEndDate()
    {
        return StartDate.AddDays(Plan.DefaultDuration);
    }

    private static double CalculateAmountToPay()
    {
        return 0;
    }
}
