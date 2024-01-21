using Auth.Application.Interfaces.Repositories;
using Auth.Application.Queries;
using Auth.Application.Responses;
using Auth.Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Auth.Application.Handlers;
public sealed class GetUserHandler : IRequestHandler<GetUserRequest, ResponseR<UserResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepositoryAsync _userRepository;

    public GetUserHandler(IUserRepositoryAsync userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ResponseR<UserResponse>> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var userFound = await _userRepository.GetByIdAsync(request.Id);

        if (userFound == null || userFound.IsDeleted == true)
            return new ResponseR<UserResponse>(false, "User not found or Deleted.");

        var response = _mapper.Map<UserResponse>(userFound);

        return new ResponseR<UserResponse>(response, "User Found");
    }
}