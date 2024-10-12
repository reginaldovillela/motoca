namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public class DeleteBikeCommandValidator : AbstractValidator<DeleteBikeCommand>
{
    public DeleteBikeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("O {PropertyName} não foi informado")
            .Length(5, 10)
            .WithMessage("O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}
