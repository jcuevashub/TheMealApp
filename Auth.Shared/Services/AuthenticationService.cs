using Auth.Application.DTOs;
using Auth.Application.Interfaces.Repositories;
using Auth.Application.Interfaces.Services;
using Auth.Application.Wrappers;
using Auth.Core.Common;
using Auth.Core.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Shared.Services;

public sealed class AuthenticationService : IAuthenticationService
{
    private const int RefreshTokenSize = 64;
    private readonly IUserRepositoryAsync _userRepositoryAsync;
    private readonly IPasswordService _passwordService;
    private readonly JwtConfig _jwtSettings;
    private readonly IUserTokenRepositoryAsync _userTokenRepositoryAsync;


    public AuthenticationService(IUserRepositoryAsync userRepositoryAsync, IPasswordService passwordService, IOptions<JwtConfig> jwtSettings, IUserTokenRepositoryAsync userTokenRepositoryAsync)
    {
        _userRepositoryAsync = userRepositoryAsync;
        _passwordService = passwordService;
        _jwtSettings = jwtSettings.Value;
        _userTokenRepositoryAsync = userTokenRepositoryAsync;
    }

    public async Task<ResponseR<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
    {
        try
        {
            var userFound = (await _userRepositoryAsync.FindByCondition(x => x!.Email == request.Email).ConfigureAwait(true)).AsQueryable().FirstOrDefault() ?? throw new ValidationException("Wrong email or password.");

            var isPasswordValid = _passwordService.VerifyPasswordHash(request.Password, Convert.FromBase64String(userFound.PasswordHash), Convert.FromBase64String(userFound.PasswordSalt));

            if (!isPasswordValid)
            {
                throw new ValidationException("Wrong email or password.");
            }

            return await AuthenticationResponse(userFound, "Login Success").ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ResponseR<AuthenticationResponse>> AuthenticationResponse(User? user, string message)
    {
        var jwtSecurityToken = await GenerateJwToken(user);
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        var userToken = GenerateRefreshToken();
        userToken.JwtToken = jwtToken;
        userToken.UserId = user!.Id;

        await _userRepositoryAsync.UpdateAsync(user).ConfigureAwait(false);


        var response = new AuthenticationResponse
        {
            AccessToken = jwtToken,
            RefreshToken = userToken.Token,
            ExpiresIn = userToken.Expires.ToString("yyyyMMddHHmmss")
        };

        return new ResponseR<AuthenticationResponse>(response, message);
    }

    public async Task<ResponseR<string>> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        var user = (await _userRepositoryAsync.FindByCondition(x => x!.Email == request.Email)
            .ConfigureAwait(false)).AsQueryable().FirstOrDefault();

        if (user == null || user.IsDeleted == true)
            throw new ValidationException("Email is not found.");

        var generatedPassword = _passwordService.GeneratePassword(8);

        _passwordService.CreatePasswordHash(generatedPassword, out var password, out var passwordSalt);

        user.PasswordHash = password;
        user.PasswordSalt = passwordSalt;

        await _userRepositoryAsync.UpdateAsync(user).ConfigureAwait(false);

        return new ResponseR<string>(true, "Please check your email for password reset instructions.");
    }

    public async Task<ResponseR<AuthenticationResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {

        var userToken =
            (await _userTokenRepositoryAsync.FindByCondition(x => x!.Token == request.Token).ConfigureAwait(false))
            .AsQueryable().FirstOrDefault();

        switch (userToken)
        {
            case null:
                throw new ValidationException("Refresh token does not exist.");

            case { IsActive: false }:
                throw new ValidationException("Refresh token has expired.");
        }

        var user = (await _userRepositoryAsync.FindByCondition(x => x!.Id == userToken!.UserId).ConfigureAwait(false))
            .AsQueryable().FirstOrDefault();

        if (user == null || user.IsDeleted == true)
            throw new ValidationException("Email is not found.");

        var jwtSecurityToken = await GenerateJwToken(user);
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        var userTokenNew = GenerateRefreshToken();
        userTokenNew.JwtToken = jwtToken;
        userTokenNew.UserId = user!.Id;


        await _userRepositoryAsync.UpdateAsync(user).ConfigureAwait(false);

        var response = new AuthenticationResponse
        {
            AccessToken = jwtToken,
            RefreshToken = userTokenNew.Token,
            ExpiresIn = userTokenNew.Expires.ToString("yyyyMMddHHmmss")
        };

        return new ResponseR<AuthenticationResponse>(response, "Token Refreshed Successful.");
    }

    public async Task<ResponseR<AuthenticationResponse>> ResetPasswordAsync(ResetPasswordRequest request)
    {

        var user = await _userRepositoryAsync.GetByIdAsync(request.Id);

        if (user == null || user.IsDeleted == true)
            throw new ValidationException("User not found.");

        if (request.NewPassword.Equals(request.OldPassword))
            throw new ValidationException("The password should be different.");

        _passwordService.CreatePasswordHash(request.NewPassword, out var passwordHash, out var passwordSalt);

        user!.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        user.UpdatedDate = DateTime.UtcNow;

        var userUpdated = await _userRepositoryAsync.UpdateAsync(user).ConfigureAwait(false);

        return await AuthenticationResponse(userUpdated, "Password Updated!");
    }


    public async Task<ResponseR<string>> RevokeTokenAsync(RevokeTokenRequest request)
    {
        var userToken =
            (await _userTokenRepositoryAsync.FindByCondition(x => x!.Token == request.Token).ConfigureAwait(false))
            .AsQueryable().FirstOrDefault();

        switch (userToken)
        {
            case null:
                throw new ValidationException("Refresh token does not exist.");

            case { IsActive: false }:
                throw new ValidationException("Refresh token has expired.");
        }

        userToken!.RevokedDate = DateTime.UtcNow;
        await _userTokenRepositoryAsync.UpdateAsync(userToken).ConfigureAwait(false);

        return new ResponseR<string>("Token Revoked Successful.");
    }

    private Task<JwtSecurityToken> GenerateJwToken(User? user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user!.FirstName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id.ToString())
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
        return Task.FromResult(jwtSecurityToken);
    }

    private UserToken GenerateRefreshToken()
    {
        var token = GenerateRandomToken();
        return new UserToken
        {
            Token = token,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.DurationInHours),
            CreatedDate = DateTime.UtcNow
        };
    }

    private static string GenerateRandomToken()
    {
        var randomBytes = new byte[RefreshTokenSize];
        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    private Task<JwtSecurityToken> GenerateCustomerJwToken(User customer)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, customer.FirstName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, customer.Email),
            new Claim("uid", customer.Id.ToString())
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
        return Task.FromResult(jwtSecurityToken);
    }
}
