using Motoca.API.Application.Riders.Commands;
using Motoca.API.Application.Riders.Models;
using Motoca.API.Application.Riders.Queries;
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

        api.MapGet("/", GetRidersAsync)
            .ProducesProblem((int)HttpStatusCode.NotAcceptable);

        api.MapGet("/{id}", GetRiderByIdAsync)
             .ProducesProblem((int)HttpStatusCode.NotAcceptable);

        api.MapPost("/", CreateRiderAsync)
            .AddEndpointFilter<ValidationFilter<CreateRiderCommand>>()
            .ProducesProblem((int)HttpStatusCode.NotAcceptable)
            .ProducesValidationProblem((int)HttpStatusCode.UnprocessableEntity); ;

        api.MapPost("/{id}/cnh", UpdateDriversLicenseRiderAsync)
            .ProducesProblem((int)HttpStatusCode.NotAcceptable)
            .ProducesValidationProblem((int)HttpStatusCode.UnprocessableEntity); ;
    }

    /// <summary>
    /// Consulta os entregadores cadastrados
    /// </summary>
    /// <param name="services"></param>
    private static async Task<Results<Ok<Rider[]>,
                              NotFound<AnyFailureResult>,
                              BadRequest<AnyFailureResult>>> GetRidersAsync(
        [AsParameters] RidersEndpointsServices services)
    {
        try
        {
            var query = new GetRidersQuery();
            var riders = await services.Mediator.Send(query);

            if (riders is null || riders.Length == 0)
                return TypedResults.NotFound(new AnyFailureResult("Dados inválidos", "Não encontramos nenhum entregador cadastrado no sistema"));

            return TypedResults.Ok(riders);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Não foi possível processar sua solicitação", ex.Message));
        }
    }

    /// <summary>
    /// Consulta um entregador cadastrado pelo Id (Identificador)
    /// </summary>
    /// <param name="id">Id da entregador</param>
    /// <param name="services"></param>
    private static async Task<Results<Ok<Rider>,
                              NotFound<AnyFailureResult>,
                              BadRequest<AnyFailureResult>>> GetRiderByIdAsync(
       [FromRoute(Name = "id")] string id,
       [AsParameters] RidersEndpointsServices services)
    {
        try
        {
            var query = new GetRiderByIdQuery(id);
            var rider = await services.Mediator.Send(query);

            if (rider is null)
                return TypedResults.NotFound(new AnyFailureResult("Dados inválidos", $"O entregador com o Id {id} não foi localizado"));

            return TypedResults.Ok(rider);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Não foi possível processar sua solicitação", ex.Message));
        }
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
    private static async Task<Results<Ok<AnySuccessWithDataResult<Rider>>,
                                      NotFound<AnyFailureResult>,
                                      BadRequest<AnyFailureResult>>> UpdateDriversLicenseRiderAsync(
        [FromRoute(Name = "id")] string id,
        [FromBody] UpdateDriversLicenseRiderCommand command,
        [AsParameters] RidersEndpointsServices services)
    {
        try
        {
            command.Id = id;

            var rider = await services.Mediator.Send(command);

             if (rider is null)
                return TypedResults.NotFound(new AnyFailureResult("Dados inválidos", $"O entregador com o Id {id} não foi localizado"));

            return TypedResults.Ok(new AnySuccessWithDataResult<Rider>("CNH modificada com sucesso", rider));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Dados inválidos", ex.Message));
        }
    }

}
