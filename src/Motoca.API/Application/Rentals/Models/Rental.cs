using Motoca.API.Application.Base.Models;
using Motoca.SharedKernel.Attributes;

namespace Motoca.API.Application.Rentals.Models;

/// <summary>
/// Dados da locação cadastrada no sistema
/// </summary>
/// <param name="InternalId">Id Interno do sistema</param>
/// <param name="Id">Id da locação</param>
/// <param name="RiderId">Id do entregador</param>
/// <param name="BikeId">Id da moto</param>
/// <param name="Plan">Plano contratado</param>
/// <param name="CreateAt">Data/Hora criação da locação</param>
/// <param name="StartDate">Data início da locação</param>
/// <param name="ExpectedEndDate">Data previsão da locação</param>
/// <param name="ReturnDate">Data/Hora devolução da locação</param>
[DisplayName("Locação > Dados Cadastrados")]
public record Rental(
    Guid InternalId,
    [property: JsonPropertyName("identificador"), DefaultValue("entregador-123-moto-123"), Required] string Id,
    [property: JsonPropertyName("entregador_id"), DefaultValue("entregador-123"), Required] string RiderId,
    [property: JsonPropertyName("moto_id"), DefaultValue("moto-123"), Required] string BikeId,
    [property: JsonPropertyName("plano"), Required] Plan Plan,
    [property: JsonPropertyName("data_criacao"), DefaultDateTime, Required] DateTime CreateAt,
    [property: JsonPropertyName("data_inicio"), DefaultDateOnly, Required] DateOnly StartDate,
    [property: JsonPropertyName("data_previsao_termino"), DefaultDateOnly, Required] DateOnly ExpectedEndDate,
    [property: JsonPropertyName("data_devolucao"), DefaultDateTime] DateTime? ReturnDate
) : BaseModelRecord(InternalId);
