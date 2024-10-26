using Motoca.Domain.SeedWork;
using Motoca.Domain.SeedWork.Interfaces;
using PowerXtensions.DotNet;

namespace Motoca.Domain.Rentals.AggregatesModel;

public class RentalEntity
    : Entity, IAggregateRoot
{
    private const double PenaltyValuePerDay = 50;

    public string Id { get; init; }

    public string RiderId { get; init; }

    public string BikeId { get; init; }

    public Guid PlanEntityId { get; private set; }

    public DateTime CreateAt { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime ExpectedEndDate => CalculateExpectedEndDate();

    public DateTime? ReturnDate { get; private set; }

    public bool IsActive => CalculateIsActive();

    public bool IsOverDue => CalculateIsOverDue();

    public ushort DaysOverDue => CalculateDaysOverDue();

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

        CreateAt = DateTime.UtcNow;
        StartDate = DateTime.UtcNow.AddDays(1).FirstTimeOfDay();
    }

    private DateTime CalculateExpectedEndDate()
    {
        return StartDate.AddDays(Plan.DefaultDuration).LastTimeOfDay();
    }

    private bool CalculateIsActive()
    {
        return !ReturnDate.HasValue;
    }

    private bool CalculateIsOverDue()
    {
        if (!ReturnDate.HasValue)
            return DateTime.UtcNow.LastTimeOfDay() > ExpectedEndDate;

        return ReturnDate.Value.LastTimeOfDay() > ExpectedEndDate;
    }

    private ushort CalculateDaysOverDue()
    {
        var dateBase = ReturnDate ??= DateTime.Today;

        if (IsOverDue)
            return (ushort)(dateBase - ExpectedEndDate).TotalDays;

        return 0;
    }

    private double CalculateAmountToPay()
    {
        var vpp = Plan.ValuePerDay;
        var endDay = ReturnDate ??= DateTime.UtcNow.LastTimeOfDay();
        var totalDaysDefault = (ExpectedEndDate - StartDate).TotalDays;
        var totalDaysInRental = (endDay - StartDate).TotalDays;
        var penaltyPercent = Plan.PenaltyPercent / 100 * 1;

        if (IsActive && !IsOverDue)
            return totalDaysInRental * vpp;

        if (IsOverDue)
            return (totalDaysDefault * vpp) + (DaysOverDue * PenaltyValuePerDay);

        if (IsActive)
            return (totalDaysInRental * vpp) + ((totalDaysDefault - totalDaysInRental) * penaltyPercent);

        return 0;
    }
}
