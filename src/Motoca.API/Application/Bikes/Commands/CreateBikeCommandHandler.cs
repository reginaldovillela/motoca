using MediatR;

namespace Motoca.API.Application.Bikes.Commands;

public class CreateBikeCommandHandler : IRequestHandler<CreateBikeCommand, CreateBikeCommandResult>
{
    public async Task<CreateBikeCommandResult> Handle(CreateBikeCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new CreateBikeCommandResult());
    }
}