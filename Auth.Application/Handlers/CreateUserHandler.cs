using Auth.Application.Commands;
using Auth.Application.DTOs;
using Auth.Application.Interfaces.Repositories;
using Auth.Application.Interfaces.Services;
using Auth.Application.Wrappers;
using Auth.Core.Entities;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Auth.Application.Handlers;

public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, Response<AuthenticationResponse>>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepositoryAsync _userRepositoryAsync;

    public CreateUserHandler(
        IUnitOfWork unitOfWork,
        IUserRepositoryAsync userRepositoryAsync,
        IMapper mapper,
        IPasswordService passwordService,
        IAuthenticationService authenticationService
    )
    {
        _unitOfWork = unitOfWork;
        _userRepositoryAsync = userRepositoryAsync;
        _mapper = mapper;
        _passwordService = passwordService;
        _authenticationService = authenticationService;
    }

    public async Task<Response<AuthenticationResponse>> Handle(CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var userFound = (await _userRepositoryAsync.FindByCondition(x => x.Email.ToLower() == request.Email.ToLower()).ConfigureAwait(false)).AsQueryable().FirstOrDefault();
        if (userFound != null)
            throw new ValidationException("Email is already in use.");

        var user = _mapper.Map<User>(request);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.CreatedDate = DateTime.UtcNow;

        _passwordService.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        await _userRepositoryAsync.AddAsync(user);
        await _unitOfWork.Save(cancellationToken);

        return await _authenticationService.AuthenticationResponse(user, "");
    }
}