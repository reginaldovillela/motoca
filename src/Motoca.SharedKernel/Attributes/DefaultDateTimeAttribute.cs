namespace Motoca.SharedKernel.Attributes;

public class DefaultDateTimeAttribute(int addDays = 0,
                                int addMonths = 0,
                                int addYears = 0) : DefaultValueAttribute(DateTime.UtcNow.AddDays(addDays)
                           .AddMonths(addMonths)
                           .AddYears(addYears))
{
}