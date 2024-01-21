using EventBus.Messages.Events;
using MassTransit;

namespace Email.Infrastructure.EventBusConsumer;

public class UserCreatedConsumer : IConsumer<UserCreated>
{
    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        Console.WriteLine(context.Message);
    }
}
