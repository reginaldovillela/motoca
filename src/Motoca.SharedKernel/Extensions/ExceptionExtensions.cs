namespace Motoca.SharedKernel.Extensions;

public static class ExceptionExtensions
{
    public static string ReadAll(this Exception ex)
    {
        if (ex == null)
            return "";

        if (ex.InnerException is null)
            return ex.Message;

        return $"{ex.Message} > {ReadAll(ex.InnerException)}";
    }
}
