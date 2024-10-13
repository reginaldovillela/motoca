using System.Net;

namespace Motoca.API.Filters;

internal class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var argToValidate = context.Arguments.SingleOrDefault(x => x!.GetType() == typeof(T));

        if (argToValidate is null)
        {
              return Results.Problem("Não possível processar",
                    statusCode: (int)HttpStatusCode.BadRequest);
        }

        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

        if (validator is not null)
        {
            var validationResult = await validator.ValidateAsync((T)argToValidate!);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary(),
                    statusCode: (int)HttpStatusCode.UnprocessableEntity);
            }
        }

        return await next.Invoke(context);
    }
}