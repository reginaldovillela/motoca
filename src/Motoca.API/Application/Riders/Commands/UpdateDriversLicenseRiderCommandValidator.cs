namespace Motoca.API.Application.Riders.Commands;

#pragma warning disable 1591
public class UpdateDriversLicenseRiderCommandValidator : AbstractValidator<UpdateDriversLicenseRiderCommand>
{
    private const string MensagemNuloVazio = "O {PropertyName} não foi informado";
    private const string MensagemTamanhoEntre = "O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres";
    private const string Id = "identificador";
    private const string DriversLicenseImage = "imagem_cnh";

    public UpdateDriversLicenseRiderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(Id).OverridePropertyName(Id)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(Id).OverridePropertyName(Id)
            .Length(5, 50).WithMessage(MensagemTamanhoEntre).WithName(Id).OverridePropertyName(Id);


        RuleFor(x => x.DriversLicenseImage)
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(DriversLicenseImage).OverridePropertyName(DriversLicenseImage)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(DriversLicenseImage).OverridePropertyName(DriversLicenseImage);
    }
}
