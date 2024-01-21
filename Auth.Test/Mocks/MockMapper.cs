using Auth.Application.Commands;
using Auth.Application.Responses;
using Auth.Core.Entities;
using AutoMapper;
using Moq;

namespace Auth.Test.Mocks;

public static class MockMapper
{
    public static Mock<IMapper> GetMockMapper()
    {

        var mockMapper = new Mock<IMapper>();
        //
        mockMapper.Setup(mapper => mapper.Map<User>(It.IsAny<CreateUserRequest>()))
            .Returns(new User());


        mockMapper.Setup(mapper => mapper.Map<UserResponse>(It.IsAny<User>()))
            .Returns(new UserResponse());


        return mockMapper;
    }
}
