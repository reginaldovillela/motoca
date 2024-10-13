using Motoca.SharedKernel.Attributes;

namespace Motoca.API.Application.Rentals.Models;

/// <summary>
/// Dados da locação cadastrada no sistema
/// </summary>
/// <param name="Id">Id da locação</param>
/// <param name="ValuePerDay">Valor da diária</param>
/// <param name="RiderId">Id do entregador</param>
/// <param name="BikeId">Id da moto</param>
/// <param name="StartDate">Data/Hora início da locação</param>
/// <param name="EndDate">Data/Hora fim da locação</param>
/// <param name="ExpectedEndDate">Data/hora previsão da locação</param>
/// <param name="ReturnDate">Data/Hora devolução da locação</param>
[DisplayName("Locação > Dados Cadastrados")]
public record Rental(
    [property:JsonPropertyName("identificador"), DefaultValue("0f8fad5b-d9cb-469f-a165-70867728950e"), Required] Guid Id,
    [property:JsonPropertyName("valor_diaria"), DefaultValue(10.0), Required] double ValuePerDay,
    [property:JsonPropertyName("entregador_id"), DefaultValue("entregador-123"), Required] string RiderId,
    [property:JsonPropertyName("moto_id"), DefaultValue("moto-123"), Required] string BikeId,
    [property:JsonPropertyName("data_inicio"), DefaultDateTime, Required] DateTime StartDate,
    [property:JsonPropertyName("data_termino"), DefaultDateTime, Required] DateTime EndDate,
    [property:JsonPropertyName("data_previsao_termino"), DefaultDateTime, Required] DateTime ExpectedEndDate,
    [property:JsonPropertyName("data_devolucao"), DefaultDateTime] DateTime? ReturnDate
);
