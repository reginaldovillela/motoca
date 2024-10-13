using Motoca.API.Application.Riders.Models;
using Motoca.SharedKernel.Attributes;

namespace Motoca.API.Application.Riders.Commands;

/// <summary>
/// Dados para cadastrar um entregador
/// </summary>
[DisplayName("Entregador > Dados Cadastrar")]
public class CreateRiderCommand : IRequest<Rider>
{
    /// <summary>
    /// Id do entregador a ser cadastrado no sistema
    /// </summary>
    [DefaultValue("entregador-123")]
    [JsonPropertyName("identificador")]
    [Required]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Nome do entregador a ser cadastrado no sistema
    /// </summary>
    [DefaultValue("João da Silva")]
    [JsonPropertyName("nome")]
    [Required]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// CPF do entregador a ser cadastrado no sistema
    /// </summary>
    [DefaultValue("000.000.000-00")]
    [JsonPropertyName("cpf")]
    [Required]
    public string SocialId { get; init; } = string.Empty;

    /// <summary>
    /// Data de nascimento do entregador a ser cadastrado no sistema
    /// </summary>
    [DefaultDateOnly]
    [JsonPropertyName("data_nascimento")]
    [Required]
    public DateOnly BirthDate { get; init; } = DateOnly.FromDateTime(DateTime.UtcNow);

    /// <summary>
    /// Número da CNH do entregador a ser cadastrado no sistema
    /// </summary>
    [DefaultValue("12345678900")]
    [JsonPropertyName("numero_cnh")]
    [Required]
    public string DriversLicenseNumber { get; init; } = string.Empty;

    /// <summary>
    /// Categoria da CNH do entregador a ser cadastrado no sistema
    /// </summary>
    [DefaultValue("A")]
    [JsonPropertyName("tipo_cnh")]
    [Required]
    public string DriversLicenseLevel { get; init; } = string.Empty;

    /// <summary>
    /// Image (Base64) da CNH do entregador a ser cadastrado no sistema
    /// </summary>
    [DefaultValue("base64string")]
    [JsonPropertyName("imagem_cnh")]
    [Required]
    public string DriversLicenseImage { get; init; } = string.Empty;
}
