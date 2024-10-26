using Motoca.Domain.Riders.Exceptions;
using Motoca.Domain.SeedWork;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Riders.AggregatesModel;

public class RiderEntity
    : Entity, IAggregateRoot
{
    public string Id { get; init; }

    public string Name { get; init; }

    public DateOnly BirthDate { get; private set; }

    public SocialIdVO SocialId { get; private set; } = null!;

    public DriversLicenseEntity DriversLicense { get; private set; } = null!;

    // ef required
#pragma warning disable CS8618
    protected RiderEntity() { }
#pragma warning restore CS8618

    public RiderEntity(string id, string name, DateOnly birthDate)
    {
        Id = id;
        Name = name;
        SetBirthDate(birthDate);
    }

    public void SetBirthDate(DateOnly birthDate)
    {
        var totalDays = (DateTime.Today - birthDate.ToDateTime(TimeOnly.MinValue)).TotalDays;

        var totalYears = totalDays / 365.2425;

        if (totalYears < 18)
            throw new RidersDomainException(EntityId, Id, "O entregador precisa ser maior de idade");

        BirthDate = birthDate;
    }

    public void SetSocialId(string socialIdNumber)
    {
        SocialId = new SocialIdVO(socialIdNumber);
    }

    public void SetDriversLicense(string number, string category)
    {
        DriversLicense = new DriversLicenseEntity(number, category);
    }
}
