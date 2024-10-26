using System.Runtime.Serialization;
using Motoca.Domain.SeedWork;

namespace Motoca.Domain.Riders.AggregatesModel;

public class SocialIdVO : ValueObject<SocialIdVO>
{
    [IgnoreDataMember]
    public Guid RiderEntityId { get; init; }

    public string Number { get; init; }

    // ef required
#pragma warning disable CS8618
    protected SocialIdVO() { }
#pragma warning restore CS8618

    public SocialIdVO(string number)
    {
        Number = number;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
