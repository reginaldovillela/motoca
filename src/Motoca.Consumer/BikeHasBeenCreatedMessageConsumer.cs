using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Motoca.SharedKernel.Message;

namespace Motoca.Consumer;

public class BikeHasBeenCreatedMessageConsumer : IConsumer<BikeHasBeenCreatedMessage>
{
    public async Task Consume(ConsumeContext<BikeHasBeenCreatedMessage> context)
    {
        Console.WriteLine($"NotificationCreated event consumed. Message: {context.Message}");
    }
}
