using Motoca.SharedKernel.Application.Models;

namespace Motoca.SharedKernel.Message;

public class GetRiderByIdResponse
{
    public string? ErrorMessage { get; init; }

    public Rider Rider { get; init; } = null!;
}
