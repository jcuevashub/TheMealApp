using System.ComponentModel.DataAnnotations;

namespace Auth.Application.DTOs;

public class ResetPasswordRequest
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string OldPassword { get; set; }

    [Required]
    public string NewPassword { get; set; }
}

