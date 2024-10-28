using MassTransit;
using Motoca.Domain.Bikes.AggregatesModel;
using Motoca.SharedKernel.Application.Models;
using Motoca.SharedKernel.Extensions;
using Motoca.SharedKernel.Message;

namespace Motoca.API.Services.Bikes;

#pragma warning disable 1591
public class GetBikeByIdConsumer(ILogger<GetBikeByIdConsumer> logger,
                                 IBikesRepository repository) : IConsumer<GetBikeByIdRequest>
{
    public async Task Consume(ConsumeContext<GetBikeByIdRequest> context)
    {
        try
        {
            var bike = await repository.GetByIdAsync(context.Message.BikeId, new CancellationToken());

            if (bike is null)
            {
                logger.LogInformation("Não foi encontrada a moto com o Id: {@BikeId}", context.Message.BikeId);

                await context.RespondAsync(
                    new GetBikeByIdResponse
                    {
                        ErrorMessage = $"Não foi encontrada a moto com o Id: {context.Message.BikeId}",
                    });
            }
            else
            {
                var getBikeByIdResponse = new GetBikeByIdResponse
                {
                    Bike = new Bike(bike!.EntityId,
                                    bike.Id,
                                    bike.Year,
                                    bike.Model,
                                    bike.LicensePlate)
                };

                await context.RespondAsync(getBikeByIdResponse);
            }
        }
        catch (Exception ex)
        {
            await context.RespondAsync(
                new GetBikeByIdResponse
                {
                    ErrorMessage = ex.ReadAll()
                });
        }
    }
}
