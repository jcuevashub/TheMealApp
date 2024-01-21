using Auth.Application.DTOs;
using Auth.Application.Wrappers;
using Auth.Core.Entities;

namespace Auth.Application.Interfaces.Services;

public interface IAuthenticationService
{
    Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request);
    Task<Response<AuthenticationResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<Response<AuthenticationResponse>> AuthenticationResponse(User user, string message);
    Task<Response<string>> RevokeTokenAsync(RevokeTokenRequest request);
    Task<Response<string>> ForgotPasswordAsync(ForgotPasswordRequest request);
    Task<Response<AuthenticationResponse>> ResetPasswordAsync(ResetPasswordRequest resetPassword);

}

