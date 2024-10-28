namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public class ChangeLicensePlateBikeCommandValidator : AbstractValidator<ChangeLicensePlateBikeCommand>
{
    private const string MensagemNuloVazio = "O {PropertyName} não foi informado";
    private const string MensagemTamanhoEntre = "O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres";
    private const string Id = "identificador";
    private const string LicensePlate = "placa";

    public ChangeLicensePlateBikeCommandValidator()
    {
        RuleFor(x => x.Id)
            
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(Id).OverridePropertyName(Id)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(Id).OverridePropertyName(Id)
            .Length(5, 10).WithMessage(MensagemTamanhoEntre).WithName(Id).OverridePropertyName(Id);

        RuleFor(x => x.LicensePlate)
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(LicensePlate).OverridePropertyName(LicensePlate)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(LicensePlate).OverridePropertyName(LicensePlate)
            .Matches("^[A-Z]{3}[0-9][0-9A-Z][0-9]{2}$").WithMessage("A {PropertyName} precisa estar no formato XXX0X00").WithName(LicensePlate).OverridePropertyName(LicensePlate);
    }
}
