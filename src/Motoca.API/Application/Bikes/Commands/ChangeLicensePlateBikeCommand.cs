using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Motoca.API.Application.Bikes.Models;

namespace Motoca.API.Application.Bikes.Commands;

/// <summary>
/// Dados para alterar a placa
/// </summary>
public class ChangeLicensePlateBikeCommand : IRequest<Bike>
{
    /// <summary>
    /// O Id da moto no sistema
    /// </summary>
    [JsonIgnore]
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Informe a nova placa da moto
    /// </summary>
    [DefaultValue("XXX0X00")]
    [JsonPropertyName("placa")]
    [Required]
    public required string LicensePlate { get; set; } = string.Empty;
}