namespace Motoca.SharedKernel.Attributes;

public class DefaultCurrentYearAttribute : DefaultValueAttribute
{
    public DefaultCurrentYearAttribute()
        : base(DateTime.Today.Year)
    { }
}