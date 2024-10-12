using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Motoca.API.Application.Bikes.Models;

namespace Motoca.API.Application.Bikes.Commands;

/// <summary>
/// Dados da moto que será cadastrada no sistema
/// </summary>
public class CreateBikeCommand : IRequest<Bike>
{
    /// <summary>
    /// Informe o Id (Identificador) da moto a ser cadastrada
    /// </summary>
    [DefaultValue("moto-123")]
    [JsonPropertyName("identificador")]
    [Required]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Informe o ano da moto a ser cadastrada
    /// </summary>
    [JsonPropertyName("ano")]
    [Required]
    [DefaultValue(2000)]
    public int Year { get; init; } = 2000;

    /// <summary>
    /// Informe o modelo da moto a ser cadastrada
    /// </summary>
    [DefaultValue("Modelo 123")]
    [JsonPropertyName("modelo")]
    [Required]
    public string Model { get; init; } = string.Empty;

    /// <summary>
    /// Informe a placa da moto a ser cadastrada
    /// </summary>
    [DefaultValue("XXX0X00")]
    [JsonPropertyName("placa")]
    [Required]
    public string LicensePlate { get; init; } = string.Empty;
}