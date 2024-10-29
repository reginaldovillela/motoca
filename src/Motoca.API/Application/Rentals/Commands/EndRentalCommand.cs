using Motoca.SharedKernel.Application.Models;
using Motoca.SharedKernel.Attributes;

namespace Motoca.API.Application.Rentals.Commands;

/// <summary>
/// Dados para cadastrar uma locação
/// </summary>
[DisplayName("Locação > Dados Finalizar")]
public class EndRentalCommand : IRequest<Rental?>
{
    /// <summary>
    /// Id da locação cadastrada no sistema
    /// </summary>
    [DefaultValue("locacao-123")]
    [JsonIgnore]
    [JsonPropertyName("id")]
    [Required]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Data de finilizacao da locação
    /// </summary>
    [DefaultDateTime]
    [JsonPropertyName("data_devolucao")]
    [Required]
    public DateTime ReturnDate { get; set; } = DateTime.UtcNow;

}
