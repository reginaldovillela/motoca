namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public class CreateBikeCommandValidator : AbstractValidator<CreateBikeCommand>
{
    private const string MensagemNuloVazio = "O {PropertyName} não foi informado";
    private const string MensagemTamanhoEntre = "O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres";
    private const string Id = "identificador";
    private const string Year = "ano";
    private const string Model = "modelo";
    private const string LicensePlate = "placa";

    public CreateBikeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(Id).OverridePropertyName(Id)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(Id).OverridePropertyName(Id)
            .Length(5, 50).WithMessage(MensagemTamanhoEntre).WithName(Id).OverridePropertyName(Id);

        RuleFor(x => x.Year)
            .GreaterThan(2000).WithMessage("Desculpe, mas a moto é muito antiga. A moto precisa ser de {ComparisonValue} em diante.").WithName(Year).OverridePropertyName(Year)
            .LessThan(DateTime.Now.Year + 1).WithMessage("O {PropertyName} {PropertyValue} não parece correto. Verifique novamente").WithName(Year).OverridePropertyName(Year);

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(Model).OverridePropertyName(Model)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(Model).OverridePropertyName(Model)
            .Length(5, 50).WithMessage(MensagemTamanhoEntre).WithName(Model).OverridePropertyName(Model);

        RuleFor(x => x.LicensePlate)
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(LicensePlate).OverridePropertyName(LicensePlate)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(LicensePlate).OverridePropertyName(LicensePlate)
            .Matches("^[A-Z]{3}[0-9][0-9A-Z][0-9]{2}$").WithMessage("A {PropertyName} precisa estar no formato XXX0X00").WithName(LicensePlate).OverridePropertyName(LicensePlate);
    }
}
