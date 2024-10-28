using Motoca.SharedKernel.Application.Models;

namespace Motoca.SharedKernel.Message;

public class GetBikeByIdResponse
{
    public string? ErrorMessage { get; init; }

    public Bike Bike { get; init; } = null!;
}
