using Motoca.SharedKernel.Attributes;

namespace Motoca.API.Application.Riders.Models;

/// <summary>
/// Dados do entregador cadastrado no sistema
/// </summary>
/// <param name="Id">Id do entregador</param>
/// <param name="Name">Nome do entregador</param>
/// <param name="SocialId">CPF do entregador</param>
/// <param name="BirthDate">Data de nascimento do entregador</param>
/// <param name="DriversLicense">Carteira de motorista (CNH) do entregador</param>
[DisplayName("Entregador > Dados Cadastrados")]
public record Rider(
    [property:JsonPropertyName("identificador"), DefaultValue("entregador-123"), Required] string Id,
    [property:JsonPropertyName("nome"), DefaultValue("João da Silva"), Required] string Name,
    [property:JsonPropertyName("cpf"), DefaultValue("João da Silva"), Required] string SocialId,
    [property:JsonPropertyName("data_nascimento"), DefaultDateOnly, Required] DateOnly BirthDate,
    [property:JsonPropertyName("cnh"), Required] DriversLicense DriversLicense
);