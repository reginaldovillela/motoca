namespace Motoca.SharedKernel.Attributes;

public class DefaultDateOnlyAttribute(int addDays = 0, 
                                      int addMonths = 0,
                                      int addYears = 0)
    : DefaultValueAttribute(
        DateOnly.FromDateTime(
            DateTime.Today.AddDays(addDays)
                          .AddMonths(addMonths)
                          .AddYears(addYears)))
{
}