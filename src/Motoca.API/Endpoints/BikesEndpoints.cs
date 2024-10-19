using Motoca.API.Application.Bikes.Commands;
using Motoca.API.Application.Bikes.Models;
using Motoca.API.Application.Bikes.Queries;
using Motoca.API.Filters;
using Motoca.API.Models.Results;

namespace Motoca.API.Endpoints;

#pragma warning disable 1591
public class BikesEndpointsServices(IMediator mediator,
                                    ILogger<BikesEndpointsServices> logger)
{
    public IMediator Mediator { get; set; } = mediator;

    public ILogger<BikesEndpointsServices> Logger { get; } = logger;
}

public static class BikesEndpoints
{
    private const string TagEndpoint = "Motos";
    private const string BaseEndpoint = "motos";

    public static void MapBikesEndpoints(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup(BaseEndpoint)
                     .WithTags(TagEndpoint);

        api.MapGet("/", GetBikesAsync)
            .ProducesProblem((int)HttpStatusCode.NotAcceptable);

        api.MapGet("/{id}", GetBikeByIdAsync)
             .ProducesProblem((int)HttpStatusCode.NotAcceptable);

        api.MapPost("/", CreateBikeAsync)
            .AddEndpointFilter<ValidationFilter<CreateBikeCommand>>()
            .ProducesProblem((int)HttpStatusCode.NotAcceptable)
            .ProducesValidationProblem((int)HttpStatusCode.UnprocessableEntity);

        api.MapDelete("/{id}", DeleteBikeAsync)
            .ProducesProblem((int)HttpStatusCode.NotAcceptable)
            .ProducesValidationProblem((int)HttpStatusCode.UnprocessableEntity);

        api.MapPut("/{id}/placa", ChangeLicensePlateBikeAsync)
            .ProducesProblem((int)HttpStatusCode.NotAcceptable);
    }

    /// <summary>
    /// Consulta as motos cadastradas, com a possibilidade de filtrar pela placa
    /// </summary>
    /// <param name="licensePlate">Informe uma placa para filtrar</param>
    /// <param name="services"></param>
    private static async Task<Results<Ok<Bike[]>,
                              NotFound<AnyFailureResult>,
                              BadRequest<AnyFailureResult>>> GetBikesAsync(
        [FromQuery(Name = "placa")] string? licensePlate,
        [AsParameters] BikesEndpointsServices services)
    {
        try
        {
            var query = new GetBikesQuery(licensePlate);
            var bike = await services.Mediator.Send(query);

            if (bike is null || bike.Length == 0)
                return TypedResults.NotFound(new AnyFailureResult("Dados inválidos", "Não encontramos nenhuma moto cadastrada no sistema"));

            return TypedResults.Ok(bike);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Não foi possível processar sua solicitação", ex.Message));
        }
    }

    /// <summary>
    /// Consulta uma moto cadastrada pelo Id (Identificador)
    /// </summary>
    /// <param name="id">Id da moto</param>
    /// <param name="services"></param>
    private static async Task<Results<Ok<Bike>,
                              NotFound<AnyFailureResult>,
                              BadRequest<AnyFailureResult>>> GetBikeByIdAsync(
       [FromRoute(Name = "id")] string id,
       [AsParameters] BikesEndpointsServices services)
    {
        try
        {
            var query = new GetBikeByIdQuery(id);
            var bike = await services.Mediator.Send(query);

            if (bike is null)
                return TypedResults.NotFound(new AnyFailureResult("Dados inválidos", $"A moto com o Id {id} não foi localizada"));

            return TypedResults.Ok(bike);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Não foi possível processar sua solicitação", ex.Message));
        }
    }

    /// <summary>
    /// Cadastra uma moto no sistema
    /// </summary>
    /// <param name="command">Dados da moto para cadastrar</param>
    /// <param name="services"></param>
    private static async Task<Results<Created<AnySuccessWithDataResult<Bike>>, BadRequest<AnyFailureResult>>> CreateBikeAsync(
        [FromBody] CreateBikeCommand command,
        [AsParameters] BikesEndpointsServices services)
    {
        try
        {
            var bike = await services.Mediator.Send(command);

            return TypedResults.Created(string.Empty, new AnySuccessWithDataResult<Bike>("Moto cadastrada com sucesso", bike));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Dados inválidos", ex.Message));
        }
    }

    /// <summary>
    /// Remove/deleta uma moto do sistema pelo Id (Identificador)
    /// </summary>
    /// <param name="id">Id da moto</param>
    /// <param name="services"></param>
    private static async Task<Results<Ok<AnySuccessResult>,
                                      NotFound<AnyFailureResult>,
                                      BadRequest<AnyFailureResult>>> DeleteBikeAsync(
        [FromRoute(Name = "id")] string id,
        [AsParameters] BikesEndpointsServices services)
    {
        try
        {
            var command = new DeleteBikeCommand(id);

            var bike = await services.Mediator.Send(command);

            if (bike is null)
                return TypedResults.NotFound(new AnyFailureResult("Dados inválidos", $"A moto com o Id {id} não foi localizada"));

            return TypedResults.Ok(new AnySuccessResult($"O cadastro da moto {id} foi removido"));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Dados inválidos", ex.Message));
        }
    }

    /// <summary>
    /// Altera a placa de uma moto cadastrada no sistema pelo Id (Identificador)
    /// </summary>
    /// <param name="id">Id da moto</param>
    /// <param name="command">Dados da nova placa</param>
    /// <param name="services"></param>
    private static async Task<Results<Ok<AnySuccessWithDataResult<Bike>>,
                                      NotFound<AnyFailureResult>,
                                      BadRequest<AnyFailureResult>>> ChangeLicensePlateBikeAsync(
        [FromRoute(Name = "id")] string id,
        [FromBody] ChangeLicensePlateBikeCommand command,
        [AsParameters] BikesEndpointsServices services)
    {
        try
        {
            command.Id = id;

            var bike = await services.Mediator.Send(command);

            if (bike is null)
                return TypedResults.NotFound(new AnyFailureResult("Dados inválidos", $"A moto com o Id {id} não foi localizada"));

            return TypedResults.Ok(new AnySuccessWithDataResult<Bike>("Placa modificada com sucesso", bike));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Dados inválidos", ex.Message));
        }
    }
}
