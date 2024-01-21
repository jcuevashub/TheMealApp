using Auth.Application.DTOs;
using Auth.Application.Wrappers;
using MediatR;

namespace Auth.Application.Commands;

public sealed record CreateUserRequest(
 string Password,
 string FirstName,
 string LastName,
 string Email
) : IRequest<Response<AuthenticationResponse>>;
