using Auth.Application.DTOs;
using Auth.Shared.Services;
using Auth.Test.Mocks;
using FluentValidation;

namespace Auth.Test.Services;

public class AuthenticationServiceTest
{
    private readonly AuthenticationService _authenticationService;

    public AuthenticationServiceTest()
    {
        var userRepositoryAsync = MockUserRepositoryAsync.GetUserRepositoryAsync();
        var mockPasswordService = MockPasswordService.GetMockPasswordService();
        var mockJwtSettings = MockJwtSettings.GetMockJwtSettings();
        var mockTokenRepositoryAsync = MockTokenRepositoryAsync.GetUserRepositoryAsync();

        _authenticationService = new AuthenticationService(
            userRepositoryAsync.Object,
            mockPasswordService.Object,
            mockJwtSettings.Object,
            mockTokenRepositoryAsync.Object
        );
    }

    [Fact]
    public async Task Login_Success()
    {
        //Arrange
        var authenticationRequest = new AuthenticationRequest()
        {
            Email = "user1@example.com",
            Password = "sa12345678"
        };

        //Act
        var response = await _authenticationService.AuthenticateAsync(authenticationRequest);

        //Assets
        Assert.NotNull(response.Data);
    }

    [Fact]
    public async Task Login_Fails_With_Wrong_Email()
    {
        //Arrange
        var authenticationRequest = new AuthenticationRequest()
        {
            Email = "user2@example.com",
            Password = "sa12345678"
        };

        //Act
        var exception = await Assert.ThrowsAsync<ValidationException>(() => _authenticationService.AuthenticateAsync(authenticationRequest));

        //Assets
        Assert.Contains("Wrong email or password.", exception.Message);
    }

    [Fact]
    public async Task Login_Fails_With_Wrong_Password()
    {
        //Arrange
        var authenticationRequest = new AuthenticationRequest()
        {
            Email = "user1@example.com",
            Password = "sa12345672"
        };

        //Act
        var exception = await Assert.ThrowsAsync<ValidationException>(() => _authenticationService.AuthenticateAsync(authenticationRequest));

        //Assets
        Assert.Contains("Wrong email or password.", exception.Message);
    }

    [Fact]
    public async Task ForgotPassword_WithEmail_Success()
    {
        //Arrange
        var forgotPasswordRequest = new ForgotPasswordRequest()
        {
            Email = "social@example.com"
        };

        //Act
        var response = await _authenticationService.ForgotPasswordAsync(forgotPasswordRequest);

        //Assets
        Assert.True(response.Status);
    }

    [Fact]
    public async Task ForgotPassword_WithWrongEmail_Fails()
    {
        //Arrange
        var forgotPasswordRequest = new ForgotPasswordRequest()
        {
            Email = "user2@example.com"
        };

        //Act
        var exception = await Assert.ThrowsAsync<ValidationException>(() => _authenticationService.ForgotPasswordAsync(forgotPasswordRequest));

        //Assets
        Assert.Contains("Email is not found.", exception.Message);
    }

    [Fact]
    public async Task ResetPassword_Success()
    {
        //Arrange
        var resetPasswordDto = new ResetPasswordRequest()
        {
            Id = new Guid("f6532788-f64b-4c1c-8084-f3212962a887"),
            OldPassword = "sa12345678",
            NewPassword = "@sa12345678"
        };

        //Act
        var response = await _authenticationService.ResetPasswordAsync(resetPasswordDto);

        //Assets
        Assert.True(response.Status);
    }

    [Fact]
    public async Task ResetPassword_UserNotFound_Fails()
    {
        //Arrange
        var resetPasswordDto = new ResetPasswordRequest()
        {
            Id = new Guid("f6532788-f64b-4c1c-8084-f3212962a882"),
            OldPassword = "sa12345678",
            NewPassword = "@sa12345678"
        };

        //Act
        var exception = await Assert.ThrowsAsync<ValidationException>(() => _authenticationService.ResetPasswordAsync(resetPasswordDto));

        //Assets
        Assert.Contains("User not found.", exception.Message);
    }

    [Fact]
    public async Task ResetPassword_WithSamePassword_Fails()
    {
        //Arrange
        var resetPasswordDto = new ResetPasswordRequest()
        {
            Id = new Guid("f6532788-f64b-4c1c-8084-f3212962a887"),
            OldPassword = "sa12345678",
            NewPassword = "sa12345678"
        };

        //Act
        var exception = await Assert.ThrowsAsync<ValidationException>(() => _authenticationService.ResetPasswordAsync(resetPasswordDto));

        //Assets
        Assert.Contains("The password should be different.", exception.Message);
    }

    [Fact]
    public async Task RefreshTokenAsync_Success()
    {
        //Arrange
        var refreshTokenRequest = new RefreshTokenRequest()
        {
            Token = "Az2EGFd9QE3Wb63J3bM5HPrG/lBGPe1DZbSsUiorEcJmOvElkpO+em6kgoZyp1S9D2M5WShDCHDajOUT9n+vOw=="
        };

        //Act
        var response = await _authenticationService.RefreshTokenAsync(refreshTokenRequest);

        //Assets
        Assert.NotNull(response.Data);
        Assert.True(response.Status);
    }

    [Fact]
    public async Task RefreshTokenAsync_Expires_Fails()
    {
        //Arrange
        var refreshTokenRequest = new RefreshTokenRequest()
        {
            Token = "Nb+tDtlFtgF2a8aeOdU8BkjshCqY0NteGMc7IiETpN5cTAFYf10mNJ0wOq07ghOlVJferJUxMz4MQcu1RInl7w=="
        };

        //Act
        var exception = await Assert.ThrowsAsync<ValidationException>(() => _authenticationService.RefreshTokenAsync(refreshTokenRequest));

        //Assets
        Assert.Contains("Refresh token has expired.", exception.Message);
    }

    [Fact]
    public async Task RevokeTokenAsync_Success()
    {
        //Arrange
        var revokeTokenRequest = new RevokeTokenRequest()
        {
            Token = "Az2EGFd9QE3Wb63J3bM5HPrG/lBGPe1DZbSsUiorEcJmOvElkpO+em6kgoZyp1S9D2M5WShDCHDajOUT9n+vOw=="
        };

        //Act
        var response = await _authenticationService.RevokeTokenAsync(revokeTokenRequest);

        //Assets
        Assert.NotNull(response.Data);
        Assert.True(response.Status);
    }

    [Fact]
    public async Task RevokeTokenAsync_Expires_Fails()
    {
        //Arrange
        var revokeTokenRequest = new RevokeTokenRequest()
        {
            Token = "Nb+tDtlFtgF2a8aeOdU8BkjshCqY0NteGMc7IiETpN5cTAFYf10mNJ0wOq07ghOlVJferJUxMz4MQcu1RInl7w=="
        };

        //Act
        var exception = await Assert.ThrowsAsync<ValidationException>(() => _authenticationService.RevokeTokenAsync(revokeTokenRequest));

        //Assets
        Assert.Contains("Refresh token has expired.", exception.Message);
    }
}