using Auth.Application.Commands;
using Auth.Application.DTOs;
using Auth.Core.Entities;
using AutoMapper;

namespace Auth.Application.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateUserRequest, User>();
        CreateMap<User, AuthenticationResponse>();
    }
}

