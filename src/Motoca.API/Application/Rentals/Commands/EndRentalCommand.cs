using Motoca.API.Application.Rentals.Models;
using Motoca.SharedKernel.Attributes;

namespace Motoca.API.Application.Rentals.Commands;

/// <summary>
/// Dados para cadastrar uma locação
/// </summary>
[DisplayName("Locação > Dados Finalizar")]
public class EndRentalCommand : IRequest<Rental>
{
    /// <summary>
    /// Id da locação cadastrada no sistema
    /// </summary>
    //[DefaultValue("entregador-123")]
    [JsonIgnore]
    [JsonPropertyName("id")]
    [Required]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Data de finilizacao da locação
    /// </summary>
    [DefaultDateTime]
    [JsonPropertyName("data_devolucao")]
    [Required]
    public DateTime ReturnDate { get; init; } = DateTime.UtcNow;

}
