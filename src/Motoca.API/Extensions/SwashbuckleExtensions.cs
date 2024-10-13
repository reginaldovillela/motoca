namespace Motoca.API.Extensions;

internal static class SwashbuckleExtensions
{
    public static string DefaultSchemaIdSelector(this Type type)
    {
        if (!type.IsConstructedGenericType) 
            return type.Name.Replace("[]", "Array");

        var prefix = type.GetGenericArguments()
                        .Select(DefaultSchemaIdSelector)
                        .Aggregate((previous, current) => previous + current);

        var name = type.Name.Split('`').First();

        return $"{prefix}{name}";
    }

}