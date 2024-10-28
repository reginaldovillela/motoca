using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Motoca.Domain.Bikes.Exceptions;
using Motoca.Domain.SeedWork;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Bikes.AggregatesModel;

[Index(nameof(Id), IsUnique = true)]
[Index(nameof(LicensePlate), IsUnique = true)]
[Table("bikes")]
public class BikeEntity
    : Entity, IAggregateRoot
{
    [Length(5, 50)]
    [MaxLength(50)]
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Id { get; private init; }

    [Column(TypeName = "smallint")]
    [Required]
    public ushort Year { get; private set; } = 0;

    [Length(5, 50)]
    [MaxLength(50)]
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Model { get; private set; } = null!;

    [Length(7, 7)]
    [MaxLength(7)]
    [Required]
    [StringLength(7, MinimumLength = 7)]
    public string LicensePlate { get; private set; } = null!;

    #region "ef requirements and relations"

#pragma warning disable CS8618
    protected BikeEntity() { }
#pragma warning restore CS8618

    #endregion

    public BikeEntity(string id)
    {
        Id = id;
    }

    public void SetYear(ushort year)
    {
        if (year < 2000)
            throw new BikesDomainException(EntityId, Id, "Essa motoca é muito velhinha");

        if (year > DateTime.UtcNow.Year + 1)
            throw new BikesDomainException(EntityId, Id, "Essa motoca é nova demais. Ela vem do futuro?!");

        Year = year;
    }

    public void SetModel(string model)
    {
        Model = model;
    }

    public void SetLicensePlate(string licensePlate)
    {
        LicensePlate = licensePlate;
    }
}
