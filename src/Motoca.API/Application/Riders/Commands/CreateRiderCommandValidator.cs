namespace Motoca.API.Application.Riders.Commands;

#pragma warning disable 1591
public class CreateRiderCommandValidator : AbstractValidator<CreateRiderCommand>
{
    private const string MensagemNuloVazio = "O {PropertyName} não foi informado";
    private const string MensagemTamanhoEntre = "O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres";
    private const string Id = "identificador";
    private const string Name = "nome";
    private const string SocialId = "cpf";
    private const string BirthDate = "data_nascimento";
    private const string DriversLicenseNumber = "cnh";
    private const string DriversLicenseCategory = "tipo_cnh";

    private readonly string[] driversLicenseLevelsAllowed = ["A", "B", "AB"];

    public CreateRiderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(Id).OverridePropertyName(Id)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(Id).OverridePropertyName(Id)
            .Length(5, 50).WithMessage(MensagemTamanhoEntre).WithName(Id).OverridePropertyName(Id);

        RuleFor(x => x.Name)
             .NotEmpty().WithMessage(MensagemNuloVazio).WithName(Name).OverridePropertyName(Name)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(Name).OverridePropertyName(Name)
            .Length(5, 100).WithMessage(MensagemTamanhoEntre).WithName(Name).OverridePropertyName(Name);

        RuleFor(x => x.SocialId)
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(SocialId).OverridePropertyName(SocialId)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(SocialId).OverridePropertyName(SocialId)
            .Length(11).WithMessage("O {PropertyName} precisa ter {MinLength} caracteres").WithName(SocialId).OverridePropertyName(SocialId);

        RuleFor(x => x.BirthDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-18)))
            .WithMessage("O entregador não é maior de idade").WithName(BirthDate).OverridePropertyName(BirthDate);

        RuleFor(x => x.DriversLicenseNumber)
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(DriversLicenseNumber).OverridePropertyName(DriversLicenseNumber)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(DriversLicenseNumber).OverridePropertyName(DriversLicenseNumber)
            .Length(1, 11).WithMessage(MensagemTamanhoEntre).WithName(DriversLicenseNumber).OverridePropertyName(DriversLicenseNumber);

        RuleFor(x => x.DriversLicenseCategory)
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(DriversLicenseCategory).OverridePropertyName(DriversLicenseCategory)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(DriversLicenseCategory).OverridePropertyName(DriversLicenseCategory)
            .Must(x => driversLicenseLevelsAllowed.Contains(x))
            .WithMessage("O {PropertyName} precisa ser uma das opções: " + string.Join(", ", driversLicenseLevelsAllowed))
            .WithName(DriversLicenseCategory).OverridePropertyName(DriversLicenseCategory);
    }
}
