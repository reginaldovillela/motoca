using Motoca.API.Application.Riders.Models;

namespace Motoca.API.Application.Riders.Commands;

/// <summary>
/// Dados para atualizar a CNH do entregador
/// </summary>
[DisplayName("Entregador > CNH > Dados Atualizar")]
public class UpdateDriversLicenseRiderCommand : IRequest<Rider>
{
    /// <summary>
    /// Id do entregador cadastrado no sistema
    /// </summary>
    [DefaultValue("entregador-123")]
    [JsonIgnore]
    [JsonPropertyName("identificador")]
    [Required]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Imagem (Base64) da CNH do entregador para ser atulizado no sistema
    /// </summary>
    [DefaultValue("base64string")]
    [Base64String(ErrorMessage = "Apenas imagens em Base64")]
    [JsonPropertyName("imagem_cnh")]
    [Required]
    public string DriversLicenseImage { get; set; } = string.Empty;
}
