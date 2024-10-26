using Motoca.SharedKernel.Application.Models;


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
    /// Id do plano cadastrado no sistema
    /// </summary>
    [DefaultValue("plano-123")]
    [JsonPropertyName("plano_id")]
    [Required]
    public string PlanId { get; init; } = string.Empty;
}
