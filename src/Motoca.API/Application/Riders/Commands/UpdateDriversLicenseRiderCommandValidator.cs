namespace Motoca.API.Application.Riders.Commands;

#pragma warning disable 1591
public class UpdateDriversLicenseRiderCommandValidator : AbstractValidator<UpdateDriversLicenseRiderCommand>
{
    public UpdateDriversLicenseRiderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("A {PropertyName} não pode ser vazia")
            .NotNull()
            .WithMessage("O {PropertyName} não foi informado")
            .Length(5, 10)
            .WithMessage("O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(x => x.DriversLicenseImage)
            .NotEmpty()
            .WithMessage("A {PropertyName} não pode ser vazia")
            .NotNull()
            .WithMessage("O {PropertyName} não foi informado");
    }
}
