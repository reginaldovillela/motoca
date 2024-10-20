namespace Motoca.API.Application.Rentals.Commands;

#pragma warning disable 1591
public class CreateRentalCommandValidator : AbstractValidator<CreateRentalCommand>
{
    public CreateRentalCommandValidator()
    {
        RuleFor(x => x.RiderId)
            .NotEmpty()
            .NotNull()
            .WithMessage("O {PropertyName} não foi informado")
            .Length(5, 50)
            .WithMessage("O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        
        RuleFor(x => x.BikeId)
            .NotEmpty()
            .NotNull()
            .WithMessage("O {PropertyName} não foi informado")
            .Length(5, 50)
            .WithMessage("O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

         RuleFor(x => x.PlanId)
            .NotEmpty()
            .NotNull()
            .WithMessage("O {PropertyName} não foi informado")
            .Length(5, 50)
            .WithMessage("O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}
