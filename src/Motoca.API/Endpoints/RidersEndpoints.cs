using Microsoft.AspNetCore.Http.HttpResults;
using Motoca.API.Application.Riders.Commands;
using Motoca.API.Application.Riders.Models;

namespace Motoca.API.Endpoints;

#pragma warning disable 1591
public class RidersEndpointsServices(IMediator mediator,
                                     ILogger<RidersEndpointsServices> logger)
{
    public IMediator Mediator { get; set; } = mediator;

    public ILogger<RidersEndpointsServices> Logger { get; } = logger;
}

public static class RidersEndpoints
{
    private const string TagEndpoint = "Entregadores";
    private const string BaseEndpoint = "entregadores";

    public static void MapRidersEndpoints(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup(BaseEndpoint)
                     .WithTags(TagEndpoint);

        api.MapPost("/", CreateRiderAsync);

        api.MapPost("/{id}/cnh", UpdateDriversLicenseRiderAsync);
    }

    /// <summary>
    /// Cadastra uma moto no sistema
    /// </summary>
    /// <param name="command">Dados da moto para cadastrar</param>
    /// <param name="services"></param>
    private static async Task<Results<Created<Rider>, BadRequest<string>>> CreateRiderAsync(
        [FromBody] CreateRiderCommand command,
        [AsParameters] RidersEndpointsServices services)
    {
        try
        {
            var rental = await services.Mediator.Send(command);

            return TypedResults.Created(string.Empty, rental);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Altera a placa de uma moto cadastrada no sistema pelo Id (Identificador)
    /// </summary>
    /// <param name="id">Id da moto</param>
    /// <param name="command">Dados da nova placa</param>
    /// <param name="services"></param>
    private static async Task<Results<Ok<string>, BadRequest<string>>> UpdateDriversLicenseRiderAsync(
        [FromRoute(Name = "id")] string id,
        [FromBody] UpdateDriversLicenseRiderCommand command,
        [AsParameters] RidersEndpointsServices services)
    {
        try
        {
            //command.Id = id;

            var rental = await services.Mediator.Send(command);

            return TypedResults.Ok("");
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

}
