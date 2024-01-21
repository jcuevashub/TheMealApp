using System.ComponentModel.DataAnnotations;

namespace Auth.Application.DTOs;

public class RevokeTokenRequest
{
    [Required(ErrorMessage = "Token is Required")]
    public virtual string Token { get; set; }
}

