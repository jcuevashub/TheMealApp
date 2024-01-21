using System.ComponentModel.DataAnnotations;

namespace Auth.Application.DTOs;

public sealed class AuthenticationRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

}

