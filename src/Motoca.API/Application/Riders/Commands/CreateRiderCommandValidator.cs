namespace Motoca.API.Application.Riders.Commands;

#pragma warning disable 1591
public class CreateRiderCommandValidator : AbstractValidator<CreateRiderCommand>
{
    private readonly string[] driversLicenseLevelsAllowed = ["A", "B", "AB"];

    public CreateRiderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("A {PropertyName} não pode ser vazia")
            .NotNull()
            .WithMessage("O {PropertyName} não foi informado")
            .Length(5, 50)
            .WithMessage("O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("A {PropertyName} não pode ser vazia")
            .NotNull()
            .WithMessage("O {PropertyName} não foi informado")
            .Length(5, 100)
            .WithMessage("O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(x => x.SocialId)
            .NotEmpty()
            .WithMessage("A {PropertyName} não pode ser vazia")
            .NotNull()
            .WithMessage("A {PropertyName} não foi informado")
            .Length(11)
            .WithMessage("O {PropertyName} precisa ter {MinLength} caracteres");
        //.Matches("^[A-Z]{3}[0-9][0-9A-Z][0-9]{2}$")
        //.WithMessage("A {PropertyName} precisa estar no formato 000.000.000-00");

        RuleFor(x => x.BirthDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now.AddYears(-18)))
            .WithMessage("O entregador não é maior de idade");

        RuleFor(x => x.DriversLicenseNumber)
            .NotEmpty()
            .WithMessage("A {PropertyName} não pode ser vazia")
            .NotNull()
            .WithMessage("O {PropertyName} não foi informado")
            .Length(5, 15)
            .WithMessage("O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(x => x.DriversLicenseCategory)
            .NotEmpty()
            .WithMessage("A {PropertyName} não pode ser vazia")
            .NotNull()
            .WithMessage("O {PropertyName} não foi informado")
            .Must(x => driversLicenseLevelsAllowed.Contains(x))
            .WithMessage("O {PropertyName} precisa ser uma das opções: " + string.Join(", ", driversLicenseLevelsAllowed));
            //.WithMessage("O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}
