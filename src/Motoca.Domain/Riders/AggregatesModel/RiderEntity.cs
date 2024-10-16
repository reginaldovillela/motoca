using Motoca.Domain.SeedWork;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Riders.AggregatesModel;

public class RiderEntity(string id, string name, string cpfNumber)
    : Entity, IAggregateRoot
{
    public string Id { get; init; } = id;

    public string Name { get; init; } = name;

    public CPFValueObject CPF { get; init; } = new CPFValueObject(cpfNumber);

    public DateOnly BirthDate { get; private set; }

    public DriversLicenseEntity DriversLicense { get; private set; }
}
