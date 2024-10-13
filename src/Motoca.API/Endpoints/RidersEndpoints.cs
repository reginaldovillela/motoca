using Motoca.API.Application.Riders.Commands;
using Motoca.API.Application.Riders.Models;
using Motoca.API.Filters;
using Motoca.API.Models.Results;

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

        api.MapPost("/", CreateRiderAsync)
            .AddEndpointFilter<ValidationFilter<CreateRiderCommand>>()
            .ProducesProblem((int)HttpStatusCode.NotAcceptable)
            .ProducesValidationProblem((int)HttpStatusCode.UnprocessableEntity);;

        api.MapPost("/{id}/cnh", UpdateDriversLicenseRiderAsync)
            .AddEndpointFilter<ValidationFilter<UpdateDriversLicenseRiderCommand>>()
            .ProducesProblem((int)HttpStatusCode.NotAcceptable)
            .ProducesValidationProblem((int)HttpStatusCode.UnprocessableEntity);;
    }

    /// <summary>
    /// Cadastra um entregador no sistema
    /// </summary>
    /// <param name="command">Dados do entregador para cadastrar</param>
    /// <param name="services"></param>
    private static async Task<Results<Created<Rider>, BadRequest<AnyFailureResult>>> CreateRiderAsync(
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
            return TypedResults.BadRequest(new AnyFailureResult("Dados inválidos", ex.Message));
        }
    }

    /// <summary>
    /// Altera a imagem do CNH do entregador cadastrado no sistema pelo Id (Identificador)
    /// </summary>
    /// <param name="id">Id do entregador</param>
    /// <param name="command">Dados da nova imagem da CNH</param>
    /// <param name="services"></param>
    private static async Task<Results<Ok<string>, 
                                      NotFound<AnyFailureResult>, 
                                      BadRequest<AnyFailureResult>>> UpdateDriversLicenseRiderAsync(
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
            return TypedResults.BadRequest(new AnyFailureResult("Dados inválidos", ex.Message));
        }
    }

}
