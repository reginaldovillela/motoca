using Motoca.SharedKernel.Message;
using System.Text.Json;

namespace Motoca.Consumer;

public class BikeHasBeenCreatedMessageConsumer : IConsumer<BikeHasBeenCreatedMessage>
{
    public async Task Consume(ConsumeContext<BikeHasBeenCreatedMessage> context)
    {
        await Task.Run(() =>
        {
            var bike = context.Message.Bike;

            if (bike.Year >= 2024)
            {
                var bikeJson = JsonSerializer.Serialize(bike);

                Console.WriteLine($"Uma nova moto foi cadastrada: {bikeJson!}");
            }
        });
    }
}
