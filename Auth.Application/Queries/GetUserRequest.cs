using Auth.Application.Responses;
using Auth.Application.Wrappers;
using MediatR;

namespace Auth.Application.Queries;

public sealed record GetUserRequest(
    Guid Id
 ) : IRequest<Response<UserResponse>>;