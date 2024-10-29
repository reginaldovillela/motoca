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

    [Required]
    public DateTime ExpectedEndDate { get; private init; }

    public DateTime? ReturnDate { get; private set; }

    [Column(TypeName = "decimal")]
    [Required]
    public decimal AmountToPay { get; private set; }

    public bool IsActive => CalculateIsActive();

    public bool IsOverDue => CalculateIsOverDue();

    public ushort DaysOverDue => CalculateDaysOverDue();

    public ushort DaysInRental => CalculateDaysInRental();

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
        ExpectedEndDate = StartDate.AddDays(Plan.DurationTime).LastTimeOfDay();

        Id = DefineRentalId();
        AmountToPay = CalculateAmountToPay();
    }

    // construtor criado apenas para simulação de carga inicial!
    public RentalEntity(string riderId, string bikeId, PlanEntity plan, DateTime createAt)
    {
        RiderId = riderId;
        BikeId = bikeId;
        PlanEntityId = plan.EntityId;
        Plan = plan;

        CreateAt = createAt;
        StartDate = createAt.AddDays(1).FirstTimeOfDay();
        ExpectedEndDate = StartDate.AddDays(Plan.DurationTime).LastTimeOfDay();

        Id = DefineRentalId();
        AmountToPay = CalculateAmountToPay();
    }

    private string DefineRentalId()
    {
        return $"{RiderId}-{BikeId}-{CreateAt:yyyyMMddhhmmss}";
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
        if (!IsOverDue)
            return 0;

        var returnDate = ReturnDate ?? DateTime.UtcNow.LastTimeOfDay();
        var expectedEndDate = DateOnly.FromDateTime(ExpectedEndDate);
        var endDate = DateOnly.FromDateTime(returnDate);

        return (ushort)(endDate.DayNumber - expectedEndDate.DayNumber + 1);
    }

    private ushort CalculateDaysInRental()
    {
        var returnDate = ReturnDate ?? DateTime.UtcNow.LastTimeOfDay();
        var startDate = DateOnly.FromDateTime(StartDate);
        var endDate = DateOnly.FromDateTime(returnDate);

        return (ushort)(endDate.DayNumber - startDate.DayNumber + 1);
    }

    private decimal CalculateAmountToPay()
    {
        if (Plan is null)
            return 0;

        var vpp = Plan.ValuePerDay;
        var dt = Plan.DurationTime;
        var pp = Plan.PenaltyPercent / 100;
        var remainingDays = dt - DaysInRental;
        var valueOverDue = DaysOverDue * PenaltyValuePerDay;

        // está ativo
        if (IsActive)
        {
            // está atrasado
            if (IsOverDue)
                return (dt * vpp) + valueOverDue;
            else
                return DaysInRental * vpp;
        }
        else
        {
            // está atrasado
            if (IsOverDue)
                return (dt * vpp) + valueOverDue;
            else
                return (DaysInRental * vpp) + ((remainingDays * vpp) * pp);
        }
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
