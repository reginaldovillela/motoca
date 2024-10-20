using MassTransit;
using Motoca.Domain.Riders.AggregatesModel;
using Motoca.SharedKernel.Application.Models;
using Motoca.SharedKernel.Extensions;
using Motoca.SharedKernel.Message;

namespace Motoca.API.Application.Riders.Services;

#pragma warning disable 1591
public class GetRiderByIdConsumer(ILogger<GetRiderByIdConsumer> logger,
                                  IRidersRepository repository) : IConsumer<GetRiderByIdRequest>
{
    public async Task Consume(ConsumeContext<GetRiderByIdRequest> context)
    {
        try
        {



            var rider = new RiderEntity(context.Message.RiderId, "", "", DateOnly.FromDateTime(DateTime.Now));

            if (rider is null)
            {
                logger.LogInformation("Não foi encontrado o entregador com o Id: {@Id}", context.Message.RiderId);

                await context.RespondAsync(
                    new GetRiderByIdResponse
                    {
                        ErrorMessage = $"Não foi encontrado o entregador com o Id: {context.Message.RiderId}"
                    });

                return;
            }

            var getRiderByIdResponse = new GetRiderByIdResponse
            {
                Rider = new Rider(rider.EntityId,
                                  rider.Id,
                                  rider.Name,
                                  rider.SocialId.Number,
                                  rider.BirthDate,
                                  new DriversLicense(rider.EntityId,
                                                     rider.DriversLicense.Number,
                                                     rider.DriversLicense.Category,
                                                     string.Empty))
            };

            await context.RespondAsync(getRiderByIdResponse);
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
