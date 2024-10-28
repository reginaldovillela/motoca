using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Motoca.Domain.SeedWork;

namespace Motoca.Domain.Riders.AggregatesModel;

[Table("riders_drivers_licenses")]
public class DriversLicenseEntity
    : Entity
{
    [Length(1, 11)]
    [MaxLength(11)]
    [Required]
    [StringLength(11, MinimumLength = 1)]
    public string Number { get; private init; }

    [Length(1, 2)]
    [MaxLength(2)]
    [Required]
    [StringLength(2, MinimumLength = 1)]
    public string Category { get; private init; }

    #region "ef requirements and relations"

    [ForeignKey("Rider")]
    [Required]
    public Guid RiderEntityId { get; private set; }

    public RiderEntity Rider { get; private set; } = null!;

#pragma warning disable CS8618
    protected DriversLicenseEntity() { }
#pragma warning restore CS8618

    #endregion

    public DriversLicenseEntity(string number, string category)
    {
        Number = number;
        Category = category;
    }
}
