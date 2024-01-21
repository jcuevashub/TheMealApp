using System.ComponentModel.DataAnnotations;

namespace Auth.Application.DTOs;

public class RefreshTokenRequest
{
    [Required(ErrorMessage = "Token is Required")]
    public virtual string Token { get; set; }
}

