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
            .Length(5, 10)
            .WithMessage("O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        
        RuleFor(x => x.BikeId)
            .NotEmpty()
            .NotNull()
            .WithMessage("O {PropertyName} não foi informado")
            .Length(5, 10)
            .WithMessage("O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .NotNull()
            .WithMessage("O {PropertyName} não foi informado")
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("O {PropertyName} não pode estar no passado");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .NotNull()
            .WithMessage("O {PropertyName} não foi informado")
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("O {PropertyName} não pode estar no passado")
            .LessThanOrEqualTo(x => x.StartDate)
            .WithMessage("O {PropertyName} não pode ser menor que a {ComparisonProperty} ({ComparisonValue})");

        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(DateTime.Today)
            .When(x => x.ExpectedEndDate.HasValue)
            .WithMessage("O {PropertyName} não pode estar no passado")
            .LessThanOrEqualTo(x => x.StartDate)
            .When(x => x.ExpectedEndDate.HasValue)
            .WithMessage("O {PropertyName} não pode ser menor que a {ComparisonProperty} ({ComparisonValue})");

    }
}
