namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public class DeleteBikeCommandHandler : IRequestHandler<DeleteBikeCommand, bool>
{
    public async Task<bool> Handle(DeleteBikeCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(true);
    }
}