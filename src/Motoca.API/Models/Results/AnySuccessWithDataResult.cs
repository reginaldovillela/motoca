namespace Motoca.API.Models.Results;

/// <summary>
/// Modelo para qualquer sucesso
/// </summary>
/// <param name="message">Mensagem para o cliente</param>
/// <param name="data">Dados para retorno</param>
public class AnySuccessWithDataResult<T>(string message, T data)
    : AnySuccessResult(message)
{
    /// <summary>
    /// Dados para retorno
    /// </summary>
    [JsonPropertyName("dados")]
    [Required]
    public T Data { get; init; } = data;
}
