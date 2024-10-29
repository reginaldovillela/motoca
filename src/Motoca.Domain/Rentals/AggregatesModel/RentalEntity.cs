using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Motoca.Domain.SeedWork;
using Motoca.Domain.SeedWork.Interfaces;
using PowerXtensions.DotNet;

namespace Motoca.Domain.Rentals.AggregatesModel;

[Index(nameof(Id), IsUnique = true)]
[Table("rentals")]
public class RentalEntity
    : Entity, IAggregateRoot
{
    private const decimal PenaltyValuePerDay = 50;

    [MaxLength(200)]
    [Required]
    [StringLength(200)]
    public string Id { get; private init; }

    [Length(5, 50)]
    [MaxLength(50)]
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string RiderId { get; private init; }

    [Length(5, 50)]
    [MaxLength(50)]
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string BikeId { get; private init; }

    [Required]
    public DateTime CreateAt { get; private init; }

    [Required]
    public DateTime StartDate { get; private init; }

    public DateTime ExpectedEndDate => CalculateExpectedEndDate();

    public DateTime? ReturnDate { get; private set; }

    [Column(TypeName = "decimal")]
    [Required]
    public decimal AmountToPay { get; private set; }

    public bool IsActive => CalculateIsActive();

    public bool IsOverDue => CalculateIsOverDue();

    public ushort DaysOverDue => CalculateDaysOverDue();

    #region "ef requirements and relations"

    [ForeignKey("Plan")]
    [Required]
    public Guid PlanEntityId { get; private init; }

    public PlanEntity Plan { get; private set; } = null!;

#pragma warning disable CS8618
    protected RentalEntity() { }
#pragma warning restore CS8618

    #endregion

    public RentalEntity(string riderId, string bikeId, PlanEntity plan)
    {
        RiderId = riderId;
        BikeId = bikeId;
        PlanEntityId = plan.EntityId;
        Plan = plan;

        CreateAt = DateTime.UtcNow;
        StartDate = DateTime.UtcNow.AddDays(1).FirstTimeOfDay();

        Id = DefineRentalId();
        AmountToPay = CalculateAmountToPay();
    }

    private string DefineRentalId()
    {
        return $"{RiderId}-{BikeId}-{CreateAt:yyyyMMddhhmmss}";
    }

    private DateTime CalculateExpectedEndDate()
    {
        if (Plan is not null)
            return StartDate.AddDays(Plan.DurationTime).LastTimeOfDay();

        return StartDate;
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

    private decimal CalculateAmountToPay()
    {
        if (Plan is null)
            return 0;

        var vpp = Plan.ValuePerDay;
        var returnDate = ReturnDate ?? DateTime.UtcNow.LastTimeOfDay();
        var totalDaysDefault = (int)(ExpectedEndDate - StartDate).TotalDays;
        var totalDaysInRental = (int)(returnDate - StartDate).TotalDays;
        var penaltyPercent = Plan.PenaltyPercent / 100 * 1;

        // não devolvido e não atrasado
        if (IsActive && !IsOverDue)
            return totalDaysInRental * vpp;

        // não devolvido e atrasado
        if (IsActive && IsOverDue)
            return (totalDaysInRental * vpp) + ((totalDaysDefault - totalDaysInRental) * penaltyPercent);

        // devolvido e não atradado
        if (!IsActive && !IsOverDue)
            return (totalDaysInRental * vpp) + ((totalDaysDefault - totalDaysInRental) * penaltyPercent);

        // devolvido e atrasado
        if (!IsActive && IsOverDue)
            return (totalDaysDefault * vpp) + (DaysOverDue * PenaltyValuePerDay);

        return 0;
    }

    public bool EndRental(DateTime returnDate)
    {
        ReturnDate = returnDate;
        AmountToPay = CalculateAmountToPay();

        return true;
    }

    public void Recalculate()
    {
        if (IsActive && AmountToPay == 0)
            AmountToPay = CalculateAmountToPay();
    }
}
