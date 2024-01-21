using AutoMapper;
using Email.Core.Dtos;
using EventBus.Messages.Events;

namespace Auth.Application.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserCreated, UserDto>();
    }
}

