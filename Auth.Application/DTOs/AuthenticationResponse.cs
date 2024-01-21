namespace Auth.Application.DTOs;

public sealed class AuthenticationResponse
{

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public string ExpiresIn { get; set; }
}
