using EventBus.Messages.Events;
using MassTransit;

namespace Email.API.EventBusConsumer;

public class UserCreatedConsumer : IConsumer<UserCreated>
{
    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        Console.WriteLine(context.Message);
    }
}
