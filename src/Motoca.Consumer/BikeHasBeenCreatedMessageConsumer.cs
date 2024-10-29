using Motoca.SharedKernel.Message;

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
                Console.Clear();
                Console.WriteLine("******");
                Console.WriteLine("Uma nova moto foi cadastrada!");
                Console.WriteLine($"Id: {bike.Id}");
                Console.WriteLine($"Modelo: {bike.Model}");
                Console.WriteLine($"Ano: {bike.Year}");
                Console.WriteLine($"Placa: {bike.LicensePlate}");
                Console.WriteLine("******");
            }
        });
    }
}