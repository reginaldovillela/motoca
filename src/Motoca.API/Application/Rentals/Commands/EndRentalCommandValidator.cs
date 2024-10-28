namespace Motoca.API.Application.Rentals.Commands;

#pragma warning disable 1591
public class EndRentalCommandValidator : AbstractValidator<EndRentalCommand>
{
    private const string MensagemNuloVazio = "O {PropertyName} não foi informado";
    private const string ReturnDate = "data_devolucao";


    public EndRentalCommandValidator()
    {   
        RuleFor(x => x.ReturnDate)
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(ReturnDate).OverridePropertyName(ReturnDate)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(ReturnDate).OverridePropertyName(ReturnDate)
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("O {PropertyName} não pode estar no passado").WithName(ReturnDate).OverridePropertyName(ReturnDate);
    }
}
