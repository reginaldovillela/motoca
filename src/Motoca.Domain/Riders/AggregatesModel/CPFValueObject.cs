using Motoca.Domain.SeedWork;

namespace Motoca.Domain.Riders.AggregatesModel;

public class CPFValueObject(string cpfNumber) : ValueObject<CPFValueObject>
{
    public string Number { get; init; } = cpfNumber;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
