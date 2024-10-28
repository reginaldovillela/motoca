namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public class DeleteBikeCommandValidator : AbstractValidator<DeleteBikeCommand>
{
    private const string MensagemNuloVazio = "O {PropertyName} não foi informado";
    private const string MensagemTamanhoEntre = "O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres";
    private const string Id = "identificador";

    public DeleteBikeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(Id)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(Id)
            .Length(5, 50).WithMessage(MensagemTamanhoEntre).WithName(Id);
    }
}
