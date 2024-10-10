namespace Motoca.API.Application.Bikes.Commands;

public class CreateBikeCommandValidator : AbstractValidator<CreateBikeCommand>
{
    public CreateBikeCommandValidator()
    {
        RuleFor(x => x.Ano)
            .LessThanOrEqualTo(0)
            .WithMessage("O ano é muito baixo");
    }
}
