using Auth.Application.DTOs;
using Auth.Application.Interfaces.Services;
using Auth.Application.Responses;
using Auth.Application.Wrappers;
using Auth.Core.Entities;
using Moq;

namespace Auth.Test.Mocks;

public static class MockAuthenticationService
{
    public static Mock<IAuthenticationService> GetMockAuthenticationService()
    {
        var mockAuthenticationService = new Mock<IAuthenticationService>();

        var expectedUser = new User
        {
            Id = new Guid("f6532788-f64b-4c1c-8084-f3212962a88f"),
            FirstName = "User1",
            LastName = "User1",
            Email = "user1@example.com",
            PasswordHash = "aBWaVgfOgzzmt/lYHV1lR2dVxGHdmc39AfIKtXSv2Lrm7Uer1Y29TvalIowb8VgJ1KGFODm4AKWCWRQD7TxUmQ==",
            PasswordSalt =
                "YcWhryK1g1ZhXya3x3ufWtOsrrsCeyYCJJwWU/26ge3bxNPAYLcmruD8wI+MCzeWeRSiKjrcS1vyufFzFW6RL5Qzkau9OhfEjNlSIPTzdbLWXn2l0ifUPXsIhrmMcHJnujnY0XjlBQaIfWon3DgLs/qVLDfRtlBvCjJECOrd1Fk=",
        };

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

        mockAuthenticationService.Setup(service => service.AuthenticateAsync(It.IsAny<AuthenticationRequest>()))
            .ReturnsAsync(new Response<AuthenticationResponse>(expectedResponse, "Login Success"));

        mockAuthenticationService.Setup(service => service.RefreshTokenAsync(It.IsAny<RefreshTokenRequest>()))
            .ReturnsAsync(new Response<AuthenticationResponse>(expectedResponse, "Token Refreshed Successful."));

        mockAuthenticationService.Setup(service => service.RevokeTokenAsync(It.IsAny<RevokeTokenRequest>()))
            .ReturnsAsync(new Response<string>("Token Revoked Successful."));

        mockAuthenticationService.Setup(service => service.ForgotPasswordAsync(It.IsAny<ForgotPasswordRequest>()))
            .ReturnsAsync(new Response<string>("Please check your email for password reset instructions."));

        mockAuthenticationService.Setup(service => service.ResetPasswordAsync(It.IsAny<ResetPasswordRequest>()))
            .ReturnsAsync(new Response<AuthenticationResponse>(expectedResponse));

        return mockAuthenticationService;
    }
}