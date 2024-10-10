using FluentValidation;
using Motoca.API.Application.Bikes.Commands;

namespace Motoca.API.Application.IoC;

public static class ApplicationDependency
{
    public static void RegisterApplicationDependency(this IServiceCollection services)
    {
        //services.AddMediatR(s => s.RegisterServicesFromAssembly(typeof(ApplicationDependency).Assembly));

        // Adiciona os validator no modelo Singleton
        services.AddScoped<IValidator<CreateBikeCommand>, CreateBikeCommandValidator>();
    }
}
