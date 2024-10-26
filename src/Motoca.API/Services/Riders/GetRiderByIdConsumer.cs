using MassTransit;
using Motoca.Domain.Riders.AggregatesModel;
using Motoca.SharedKernel.Application.Models;
using Motoca.SharedKernel.Extensions;
using Motoca.SharedKernel.Message;

namespace Motoca.API.Services.Riders;

#pragma warning disable 1591
public class GetRiderByIdConsumer(ILogger<GetRiderByIdConsumer> logger,
                                  IRidersRepository repository) : IConsumer<GetRiderByIdRequest>
{
    public async Task Consume(ConsumeContext<GetRiderByIdRequest> context)
    {
        try
        {
            var rider = await repository.GetByIdAsync(context.Message.RiderId);

            if (rider is null)
            {
                logger.LogInformation("Não foi encontrado o entregador com o Id: {@RiderId}", context.Message.RiderId);

                await context.RespondAsync(
                    new GetRiderByIdResponse
                    {
                        ErrorMessage = $"Não foi encontrado o entregador com o Id: {context.Message.RiderId}"
                    });
            }
            else
            {
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
