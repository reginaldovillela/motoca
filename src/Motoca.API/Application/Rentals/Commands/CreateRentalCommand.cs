using Motoca.API.Application.Rentals.Models;
using Motoca.SharedKernel.Attributes;

namespace Motoca.API.Application.Rentals.Commands;

/// <summary>
/// Dados para cadastrar uma locação
/// </summary>
[DisplayName("Locação > Dados Cadastrar")]
public class CreateRentalCommand : IRequest<Rental>
{
    /// <summary>
    /// Id do entregador cadastrado no sistema
    /// </summary>
    [DefaultValue("entregador-123")]
    [JsonPropertyName("entregador_id")]
    [Required]
    public string RiderId { get; init; } = string.Empty;

    /// <summary>
    /// Id da moto cadastrada no sistema
    /// </summary>
    [DefaultValue("moto-123")]
    [JsonPropertyName("moto_id")]
    [Required]
    public string BikeId { get; init; } = string.Empty;

    /// <summary>
    /// Data de inicio da locação
    /// </summary>
    [DefaultDateTime]
    [JsonPropertyName("data_inicio")]
    [Required]
    public DateTime StartDate { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Data de finilizacao da locação
    /// </summary>
    [DefaultDateTime(30, 0, 0)]
    [JsonPropertyName("data_termino")]
    [Required]
    public DateTime EndDate { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Data de previção da finalizacao da locação
    /// </summary>
    [DefaultDateTime]
    [JsonPropertyName("data_previsao_termino")]
    public DateTime? ExpectedEndDate { get; init; } = DateTime.UtcNow;
}
