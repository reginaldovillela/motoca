namespace Motoca.API.Application.Rentals.Commands;

#pragma warning disable 1591
public class EndRentalCommandValidator : AbstractValidator<EndRentalCommand>
{
    public EndRentalCommandValidator()
    {
        
        RuleFor(x => x.ReturnDate)
            .NotEmpty()
            .NotNull()
            .WithMessage("O {PropertyName} não foi informado")
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("O {PropertyName} não pode estar no passado");
    }
}
