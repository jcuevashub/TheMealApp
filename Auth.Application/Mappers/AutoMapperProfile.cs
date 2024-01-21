using Auth.Application.Commands;
using Auth.Application.DTOs;
using Auth.Core.Entities;
using AutoMapper;
using EventBus.Messages.Events;

namespace Auth.Application.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateUserRequest, User>();
        CreateMap<CreateUserRequest, UserCreated>();
        CreateMap<User, AuthenticationResponse>();
    }
}

