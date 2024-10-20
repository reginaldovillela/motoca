using MassTransit;
using Motoca.Domain.Bikes.AggregatesModel;
using Motoca.SharedKernel.Application.Models;
using Motoca.SharedKernel.Extensions;
using Motoca.SharedKernel.Message;

namespace Motoca.API.Application.Bikes.Services;

#pragma warning disable 1591
public class GetBikeByIdConsumer(ILogger<GetBikeByIdConsumer> logger,
                                 IBikesRepository repository) : IConsumer<GetBikeByIdRequest>
{
    public async Task Consume(ConsumeContext<GetBikeByIdRequest> context)
    {
        try
        {
            var bike = new BikeEntity(context.Message.BikeId); //await repository.GetByIdAsync(context.Message.Id);

            if (bike is null)
            {
                logger.LogInformation("Não foi encontrada a moto com o Id: {@Id}", context.Message.BikeId);
                
                await context.RespondAsync(
                    new GetRiderByIdResponse
                    {
                        ErrorMessage = $"Não foi encontrada a moto com o Id: {context.Message.BikeId}"
                    });

                return;
            }

            var getBikeByIdResponse = new GetBikeByIdResponse
            {
                Bike = new Bike(bike.EntityId,
                                bike.Id,
                                bike.Year,
                                bike.Model,
                                bike.LicensePlate)
            };

            await context.RespondAsync(getBikeByIdResponse);
        }
        catch (Exception ex)
        {
            await context.RespondAsync(
                new GetRiderByIdResponse
                {
                    ErrorMessage = ex.ReadAll()
                });
        }
    }
}