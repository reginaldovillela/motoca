using Motoca.SharedKernel.Message;

namespace Motoca.Consumer;

public class BikeHasBeenCreatedMessageConsumer : IConsumer<BikeHasBeenCreatedMessage>
{
    public async Task Consume(ConsumeContext<BikeHasBeenCreatedMessage> context)
    {
        await Task.Run(() =>
        {
            Console.WriteLine($"Uma nova moto foi cadastrada: {context.Message}");
        });
    }
}
