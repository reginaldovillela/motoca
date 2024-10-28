namespace Motoca.API.Application.Rentals.Commands;

#pragma warning disable 1591
public class CreateRentalCommandValidator : AbstractValidator<CreateRentalCommand>
{
    private const string MensagemNuloVazio = "O {PropertyName} não foi informado";
    private const string MensagemTamanhoEntre = "O {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres";
    private const string RiderId = "entregador_id";
    private const string BikeId = "moto_id";
    private const string PlanId = "plano_id";

    public CreateRentalCommandValidator()
    {
        RuleFor(x => x.RiderId)
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(RiderId).OverridePropertyName(RiderId)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(RiderId).OverridePropertyName(RiderId)
            .Length(5, 50).WithMessage(MensagemTamanhoEntre).WithName(RiderId).OverridePropertyName(RiderId);
        
        RuleFor(x => x.BikeId)
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(BikeId).OverridePropertyName(BikeId)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(BikeId).OverridePropertyName(BikeId)
            .Length(5, 50).WithMessage(MensagemTamanhoEntre).WithName(BikeId).OverridePropertyName(BikeId);

         RuleFor(x => x.PlanId)
            .NotEmpty().WithMessage(MensagemNuloVazio).WithName(PlanId).OverridePropertyName(PlanId)
            .NotNull().WithMessage(MensagemNuloVazio).WithName(PlanId).OverridePropertyName(PlanId)
            .Length(5, 50).WithMessage(MensagemTamanhoEntre).WithName(PlanId).OverridePropertyName(PlanId);
    }
}
