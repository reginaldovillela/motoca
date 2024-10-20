using Motoca.SharedKernel.Application.Models;

namespace Motoca.API.Application.Bikes.Commands;

/// <summary>
/// Dados para alterar a placa
/// </summary>
[DisplayName("Moto > Placa > Dados Atualizar")]
public class ChangeLicensePlateBikeCommand : IRequest<Bike?>
{
    /// <summary>
    /// O Id da moto no sistema
    /// </summary>
    [JsonIgnore]
    [JsonPropertyName("id")]
    [FromRoute(Name = "id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Informe a nova placa da moto
    /// </summary>
    [DefaultValue("XXX0X00")]
    [JsonPropertyName("placa")]
    [Required]
    public required string LicensePlate { get; set; } = string.Empty;
}