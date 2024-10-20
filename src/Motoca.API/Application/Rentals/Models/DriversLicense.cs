using Motoca.API.Application.Base.Models;

namespace Motoca.API.Application.Rentals.Models;

/// <summary>
/// Dados da carteira de motorista nacional (CNH)
/// </summary>
/// <param name="InternalId">Id Interno do sistema</param>
/// <param name="DriversLicenseNumber">Número da carteira de motorista</param>
/// <param name="DriversLicenseCategory">Categoria da carteira de motorista</param>
/// <param name="DriversLicenseImage">Imagem (Base64) da carteira de motorista</param>
[DisplayName("Entregador > CNH > Dados Cadastrados")]
public record DriversLicense(
    Guid InternalId,
    [property:JsonPropertyName("numero"), DefaultValue("12345678900"), Required] string DriversLicenseNumber,
    [property:JsonPropertyName("tipo"), DefaultValue("A"), Required] string DriversLicenseCategory,
    [property:JsonPropertyName("imagem"), DefaultValue("base64string"), Required] string DriversLicenseImage
) : BaseModelRecord(InternalId);


