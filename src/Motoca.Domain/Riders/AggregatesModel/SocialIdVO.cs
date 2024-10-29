using Motoca.Domain.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motoca.Domain.Riders.AggregatesModel;

[ComplexType]
public class SocialIdVO : ValueObject
{
    [Column("SocialId")]
    [Length(11, 11)]
    [MaxLength(11)]
    [Required]
    [StringLength(11, MinimumLength = 11)]
    public string Number { get; private init; }

    #region "ef requirements and relations"

#pragma warning disable CS8618
    protected SocialIdVO() { }
#pragma warning restore CS8618

    #endregion

    public SocialIdVO(string number)
    {
        Number = number;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }
}
