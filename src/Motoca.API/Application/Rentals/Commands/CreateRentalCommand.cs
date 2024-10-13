using Motoca.API.Application.Rentals.Models;

namespace Motoca.API.Application.Rentals.Commands;

/// <summary>
/// Dados para cadastrar uma locação
/// </summary>
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
    [DefaultValue("moto-123")]
    [JsonPropertyName("data_inicio")]
    [Required]
    public DateTime StartDate { get; init; } = DateTime.Now;

    /// <summary>
    /// Data de finilizacao da locação
    /// </summary>
    [DefaultValue("moto-123")]
    [JsonPropertyName("data_termino")]
    [Required]
    public DateTime EndDate { get; init; } = DateTime.Now;

    /// <summary>
    /// Data de previção da finalizacao da locação
    /// </summary>
    [DefaultValue("moto-123")]
    [JsonPropertyName("data_previsao_termino")]
    public DateTime ExpectedEndDate { get; init; } = DateTime.Now;
}
