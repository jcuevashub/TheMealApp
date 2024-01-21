using AutoMapper;
using Email.Application.Interfaces.Services;
using Email.Core.Dtos;
using EventBus.Messages.Events;
using Hangfire;
using MassTransit;

namespace Email.API.EventBusConsumer;

public class UserCreatedConsumer : IConsumer<UserCreated>
{

    private readonly IMapper _mapper;

    public UserCreatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        Console.WriteLine(context.Message);
        var user = _mapper.Map<UserDto>(context.Message);

        var email = new EmailMessage
        {
            From = "TheMealApp <support@themealapp.com>",
            To = user.Email,
            Subject = "New user",
            Body = $"Welcome {user.FirstName} {user.LastName}"
        };

        BackgroundJob.Enqueue<ISendEmailJob>(job => job.ExecuteAsync(email));

    }
}
