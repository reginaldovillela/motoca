using Motoca.Domain.SeedWork;

namespace Motoca.Domain.Riders.AggregatesModel;

public class SocialIdVO : ValueObject<SocialIdVO>
{
    public string Number { get; init; }

    // ef required
    protected SocialIdVO() { }

    public SocialIdVO(string id)
    {
        Number = id;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
