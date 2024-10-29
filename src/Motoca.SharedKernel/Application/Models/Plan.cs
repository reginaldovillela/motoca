namespace Motoca.SharedKernel.Application.Models;

/// <summary>
/// Dados do plano de locação
/// </summary>
/// <param name="InternalId">Id Interno do sistema</param>
/// <param name="Id">Id do plano</param>
/// <param name="DefaultDuration">Duração padrão da locação em dias</param>
/// <param name="ValuePerDay">Valor da Diária</param>
/// <param name="PenaltyPercent">Percentual de multa</param>
[DisplayName("Locação > Planos > Dados Cadastrados")]
public record Plan(
    Guid InternalId,
    [property:JsonPropertyName("id"), DefaultValue("plano-123"), Required] string Id,
    [property:JsonPropertyName("duracao_padrao"), DefaultValue("1"), Required] ushort DefaultDuration,
    [property:JsonPropertyName("valor_diaria"), DefaultValue("0.0"), Required] decimal ValuePerDay,
    [property: JsonPropertyName("multa"), DefaultValue("0.0"), Required] decimal PenaltyPercent
) : Base(InternalId);
