using MassTransit;
using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.SharedKernel.Extensions;
using Motoca.SharedKernel.Message;

namespace Motoca.API.Services.Rentals;

#pragma warning disable 1591
public class GetBikeHasRentalsConsumer(ILogger<GetBikeHasRentalsConsumer> logger,
                                       IRentalsRepository repository) : IConsumer<GetBikeHasRentalsRequest>
{
    public async Task Consume(ConsumeContext<GetBikeHasRentalsRequest> context)
    {
		try
		{
            var rentals = await repository.BikeHasAlreadyBeenRentals(context.Message.BikeId, new CancellationToken());

            if (rentals is not null)
            {
                logger.LogInformation("A moto com o Id {@BikeId} já foi alugada e não pode ser excluida", context.Message.BikeId);

                await context.RespondAsync(
                    new GetBikeHasRentalsResponse
                    {
                        ErrorMessage = $"A moto com o Id {context.Message.BikeId} já foi alugada e não pode ser excluida",
                        HasRentals = true
                    });
            }
            else
            {
                logger.LogInformation("A moto com o Id {@BikeId} não possui histórico de locações e pode ser excluida", context.Message.BikeId);

                await context.RespondAsync(
                    new GetBikeHasRentalsResponse
                    {
                        HasRentals = false
                    });
            }
		}
		catch (Exception ex)
		{
            await context.RespondAsync(
               new GetBikeHasRentalsResponse
               {
                   ErrorMessage = ex.ReadAll()
               });
        }
    }
}
