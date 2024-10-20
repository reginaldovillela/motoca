using Motoca.SharedKernel.Attributes;

namespace Motoca.SharedKernel.Application.Models;

/// <summary>
/// Dados da moto
/// </summary>
/// <param name="InternalId">Id Interno do sistema</param>
/// <param name="Id">Id (Identificador) da moto cadastrada no sistema</param>
/// <param name="Year">Ano da moto cadastrada no sistema</param>
/// <param name="Model">Modelo da moto cadastrada no sistema</param>
/// <param name="LicensePlate">Placa da moto cadastrada no sistema</param>
[DisplayName("Moto > Dados Cadastrados")]
public record Bike(
    Guid InternalId,
    [property: JsonPropertyName("identificador"), DefaultValue("moto-123"), Required] string Id,
    [property: JsonPropertyName("ano"), DefaultCurrentYear, Required] ushort Year,
    [property: JsonPropertyName("modelo"), DefaultValue("Modelo 123"), Required] string Model,
    [property: JsonPropertyName("placa"), DefaultValue("XXX0X00"), Required] string LicensePlate)
: Base(InternalId);
