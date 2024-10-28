using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Motoca.Domain.SeedWork;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Rentals.AggregatesModel;

[Index(nameof(Id), IsUnique = true)]
[Table("rentals_plans")]
public class PlanEntity
    : Entity, IAggregateRoot
{
    [Length(5, 50)]
    [MaxLength(50)]
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Id { get; private init; }

    [Column(TypeName = "smallint")]
    [Required]
    public ushort DurationTime { get; private init; }

    [Column(TypeName = "decimal")]
    [Required]
    //[Precision(5, 2)]
    public decimal ValuePerDay { get; private init; }

    [Column(TypeName = "decimal")]
    [Required]
    //[Precision(3, 2)]
    public decimal PenaltyPercent { get; private init; }

    #region "ef requirements and relations"

    [InverseProperty("Plan")]
    public ICollection<RentalEntity> Rentals { get; private set; } = null!;

#pragma warning disable CS8618
    protected PlanEntity() { }
#pragma warning restore CS8618

    #endregion

    public PlanEntity(string id, ushort durationTime, decimal valuePerDay, decimal penaltyPercent)
    {
        Id = id;
        DurationTime = durationTime;
        ValuePerDay = valuePerDay;
        PenaltyPercent = penaltyPercent;
    }
}
