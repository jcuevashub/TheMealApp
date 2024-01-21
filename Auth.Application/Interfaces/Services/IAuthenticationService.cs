using Auth.Application.DTOs;
using Auth.Application.Wrappers;
using Auth.Core.Entities;

namespace Auth.Application.Interfaces.Services;

public interface IAuthenticationService
{
    Task<ResponseR<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request);
    Task<ResponseR<AuthenticationResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<ResponseR<AuthenticationResponse>> AuthenticationResponse(User user, string message);
    Task<ResponseR<string>> RevokeTokenAsync(RevokeTokenRequest request);
    Task<ResponseR<string>> ForgotPasswordAsync(ForgotPasswordRequest request);
    Task<ResponseR<AuthenticationResponse>> ResetPasswordAsync(ResetPasswordRequest resetPassword);

}

