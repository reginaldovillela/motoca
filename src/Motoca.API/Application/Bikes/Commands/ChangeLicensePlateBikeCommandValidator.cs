namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public class ChangeLicensePlateBikeCommandValidator : AbstractValidator<ChangeLicensePlateBikeCommand>
{
    public ChangeLicensePlateBikeCommandValidator()
    {
        // RuleFor(x => x.Identificador)
        //     .NotEmpty()
        //     .NotNull()
        //     .WithMessage("O {PropertyName} não foi informado")
        //     .Length(5, 10)
        //     .WithMessage("O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        // RuleFor(x => x.Ano)
        //     .GreaterThan(2000)
        //     .WithMessage("Desculpe, mas a moto é muito antiga. A moto precisa ser de {ComparisonValue} em diante.")
        //     .LessThan(DateTime.Now.Year + 1)
        //     .WithMessage("O {PropertyName} {ComparisonValue} não parece correto. Verifique novamente");

        // RuleFor(x => x.Modelo)
        //     .NotEmpty()
        //     .NotNull()
        //     .WithMessage("O {PropertyName} não foi informado")
        //     .Length(5, 20)
        //     .WithMessage("O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        // RuleFor(x => x.LicensePlate)
        //     .NotEmpty()
        //     .WithMessage("A {PropertyName} não pode ser vazia")
        //     .NotNull()
        //     .WithMessage("A {PropertyName} não foi informado")
        //     .Matches("^[A-Z]{3}[0-9][0-9A-Z][0-9]{2}$")
        //     .WithMessage("A {PropertyName} precisa estar no formato XXX0X00")
        //     .WithName("TTTTT");
    }
}
