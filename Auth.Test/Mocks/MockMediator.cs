using Auth.Application.Commands;
using Auth.Application.DTOs;
using Auth.Application.Responses;
using Auth.Application.Wrappers;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Test.Mocks;

public static class MockMediator
{

    public static Mock<IMediator> GetMockMediator()
    {

        var expectedResponse = new AuthenticationResponse
        {
            AccessToken = "",
            RefreshToken = "",
            ExpiresIn = ""
        };

        var userResponse = new UserResponse
        {
            Id = new Guid("f6532788-f64b-4c1c-8084-f3212962a883"),
            FirstName = "User1",
            LastName = "User1",
            Email = "social@example.com",
            IsDeleted = false,
        };

        var mockMediator = new Mock<IMediator>();

        mockMediator.Setup(m => m.Send(It.IsAny<CreateUserRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response<AuthenticationResponse>(expectedResponse));

        return mockMediator;
    }
}
