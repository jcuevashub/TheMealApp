using Auth.API.Controllers.BaseController;
using Auth.Application.Commands;
using Auth.Application.DTOs;
using Auth.Application.Interfaces.Services;
using Auth.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[Route("/v1/auth")]
public class AuthController : BaseApiController
{
    private readonly IAuthenticationService _authenticationService;
    public AuthController(IAuthenticationService authenticationService, IMediator mediator) : base(mediator)
    {
        _authenticationService = authenticationService;
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
    {
        return Ok(await _authenticationService.AuthenticateAsync(request));
    }

    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<ActionResult> SignUp(CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest tokenRequest)
    {
        return Ok(await _authenticationService.RefreshTokenAsync(tokenRequest));
    }

    [HttpPost("revoke-token")]
    public async Task<ActionResult> RevokeToken([FromBody] RevokeTokenRequest revokeToken)
    {
        return Ok(await _authenticationService.RevokeTokenAsync(revokeToken));
    }

    [HttpPost("forgot-password")]
    public async Task<ActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest forgotPasswordModel)
    {
        return Ok(await _authenticationService.ForgotPasswordAsync(forgotPasswordModel));
    }

    [HttpPost("change-password")]
    public async Task<ActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest resetPassword)
    {
        return Ok(await _authenticationService.ResetPasswordAsync(resetPassword));
    }

    [Authorize]
    [HttpGet("current-user")]
    public async Task<IActionResult> CurrentUserAsync(CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetUserRequest(GetRequestOwner()), cancellationToken);
        return Ok(response);
    }
}
