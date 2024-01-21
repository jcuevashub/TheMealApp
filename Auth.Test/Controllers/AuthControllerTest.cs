using Auth.API.Controllers;
using Auth.Application.Commands;
using Auth.Application.DTOs;
using Auth.Application.Wrappers;
using Auth.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Shouldly;

namespace Auth.Test.Controllers;

public class AuthControllerTest
{
    private readonly AuthController _authController;

    public AuthControllerTest()
    {
        var mockAuthenticationService = MockAuthenticationService.GetMockAuthenticationService();
        var mockMediator = MockMediator.GetMockMediator();

        _authController = new AuthController(
            mockAuthenticationService.Object,
            mockMediator.Object);
    }

    [Fact]
    public async Task Sign_In_ReturnsExpectedResult()
    {
        // Arrange
        var authenticationRequest = new AuthenticationRequest
        {
            Email = "user1@example.com",
            Password = "sa12345678"
        };

        // Act
        var actionResult = await _authController.AuthenticateAsync(authenticationRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var response = Assert.IsType<Response<AuthenticationResponse>>(okResult.Value);
        response.Status.ShouldBe(true);
    }

    [Fact]
    public async Task Sign_Up_ReturnsExpectedResult()
    {
        var cancellationToken = new CancellationToken();

        // Arrange
        var createUserRequest = new CreateUserRequest(
            Password: "sa12345678",
            FirstName: "User",
            LastName: "Test",
            Email: "user9@example.com"
        );

        // Act
        var actionResult = await _authController.SignUp(createUserRequest, cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var response = Assert.IsType<Response<AuthenticationResponse>>(okResult.Value);
        response.Status.ShouldBe(true);
    }

    [Fact]
    public async Task RefreshTokenAsync_ReturnsExpectedResult()
    {
        // Arrange
        var refreshTokenRequest = new RefreshTokenRequest
        {
            Token = "user1@example.com",
        };

        // Act
        var actionResult = await _authController.RefreshToken(refreshTokenRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var response = Assert.IsType<Response<AuthenticationResponse>>(okResult.Value);
        response.Status.ShouldBe(true);
    }

    [Fact]
    public async Task RevokeToken_ReturnsExpectedResult()
    {
        // Arrange
        var revokeTokenRequest = new RevokeTokenRequest
        {
            Token = "user1@example.com",
        };

        // Act
        var actionResult = await _authController.RevokeToken(revokeTokenRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var response = Assert.IsType<Response<string>>(okResult.Value);
        response.Status.ShouldBe(true);
    }

    [Fact]
    public async Task ForgotPassword_ReturnsExpectedResult()
    {
        // Arrange
        var forgotPasswordRequest = new ForgotPasswordRequest
        {
            Email = "user1@example.com",
        };

        // Act
        var actionResult = await _authController.ForgotPasswordAsync(forgotPasswordRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var response = Assert.IsType<Response<string>>(okResult.Value);
        response.Status.ShouldBe(true);
    }

    [Fact]
    public async Task ResetPasswordAsync_ReturnsExpectedResult()
    {
        // Arrange
        var resetPasswordRequest = new ResetPasswordRequest
        {
            Id = new Guid("f6532788-f64b-4c1c-8084-f3212962a88f"),
            OldPassword = "sa12345678",
            NewPassword = "sa123456789"
        };

        // Act
        var actionResult = await _authController.ResetPasswordAsync(resetPasswordRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var response = Assert.IsType<Response<AuthenticationResponse>>(okResult.Value);
        response.Status.ShouldBe(true);
    }

    [Fact]
    public async Task CurrentUserAsync_ReturnsExpectedResult()
    {
        // Arrange
        var resetPasswordRequest = new ResetPasswordRequest
        {
            Id = new Guid("f6532788-f64b-4c1c-8084-f3212962a88f"),
            OldPassword = "sa12345678",
            NewPassword = "sa123456789"
        };

        // Act
        var actionResult = await _authController.ResetPasswordAsync(resetPasswordRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var response = Assert.IsType<Response<AuthenticationResponse>>(okResult.Value);
        response.Status.ShouldBe(true);
    }

}
