using Auth.Application.Interfaces.Repositories;
using Auth.Core.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Test.Mocks;

public static class MockTokenRepositoryAsync
{
    private static readonly List<UserToken> MockTokens = new List<UserToken>
    {
        new UserToken
        {
            UserTokenId = new Guid("f6532788-f64b-4c1c-8084-f3212962a882"),
            UserId = new Guid("f6532788-f64b-4c1c-8084-f3212962a887"),
            Token = "Az2EGFd9QE3Wb63J3bM5HPrG/lBGPe1DZbSsUiorEcJmOvElkpO+em6kgoZyp1S9D2M5WShDCHDajOUT9n+vOw==",
            JwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKYWNrc29uIiwianRpIjoiZTM5NTA5ZjctYjQ4MC00ZWVkLThiOTUtZmYyNzRiZmI1ODJhIiwiZW1haWwiOiJqYWNrc29uLmN1ZXZhc0Bob3RtYWlsLmNvbSIsInVpZCI6ImY2NTMyNzg4LWY2NGItNGMxYy04MDg0LWYzMjEyOTYyYTg4ZiIsImV4cCI6MTY5OTk3NTA3NCwiaXNzIjoiV2FzaFBhc3MiLCJhdWQiOiJXYXNoUGFzcyJ9.29tTinNIKhFQxwLHDfUS_jA-4YJp-IOaJJ5HSnG09T4",
            Expires = DateTime.Parse("2025-12-08 20:53:46.379669"),
            ReplacedByToken = null,
            CreatedDate =DateTime.Parse("2023-12-08 20:53:46.379669"),
            RevokedDate =null,
            RevokedByIp = "127.0.0.2",
        },
        new UserToken
        {
            UserTokenId = new Guid("d7f65b34-3504-46b6-86ff-28c8d2e18a6d"),
            UserId = new Guid("f6532788-f64b-4c1c-8084-f3212962a88f"),
            Token = "Nb+tDtlFtgF2a8aeOdU8BkjshCqY0NteGMc7IiETpN5cTAFYf10mNJ0wOq07ghOlVJferJUxMz4MQcu1RInl7w==",
            JwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKYWNrc29uIiwianRpIjoiNDM4MDRkNzgtNjUwNS00YmE5LWJmZGEtYzA2N2JiNWU2M2M2IiwiZW1haWwiOiJqYWNrc29uLmN1ZXZhc0Bob3RtYWlsLmNvbSIsInVpZCI6ImY2NTMyNzg4LWY2NGItNGMxYy04MDg0LWYzMjEyOTYyYTg4ZiIsImV4cCI6MTcwMDE0NjMzOCwiaXNzIjoiV2FzaFBhc3MiLCJhdWQiOiJXYXNoUGFzcyJ9.9lttlPEKa0M8CkMXf01JEPL0UvDwCdAF7y8Jz6DU8pI",
            Expires = DateTime.UtcNow,
            ReplacedByToken = null,
            CreatedDate = DateTime.UtcNow,
            RevokedDate =null,
            RevokedByIp = "127.0.0.1",
        },
        new UserToken
        {
            UserTokenId = new Guid("d7f65b34-3504-46b6-86ff-28c8d2e18a6d"),
            UserId = new Guid("f6532788-f64b-4c1c-8084-f3212962a887"),
            Token = "aNb+tDtlFtgF2a8aeOdU8BkjshCqY0NteGMc7IiETpN5cTAFYf10mNJ0wOq07ghOlVJferJUxMz4MQcu1RInl7w==",
            JwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKYWNrc29uIiwianRpIjoiNDM4MDRkNzgtNjUwNS00YmE5LWJmZGEtYzA2N2JiNWU2M2M2IiwiZW1haWwiOiJqYWNrc29uLmN1ZXZhc0Bob3RtYWlsLmNvbSIsInVpZCI6ImY2NTMyNzg4LWY2NGItNGMxYy04MDg0LWYzMjEyOTYyYTg4ZiIsImV4cCI6MTcwMDE0NjMzOCwiaXNzIjoiV2FzaFBhc3MiLCJhdWQiOiJXYXNoUGFzcyJ9.9lttlPEKa0M8CkMXf01JEPL0UvDwCdAF7y8Jz6DU8pI",
            Expires = DateTime.UtcNow,
            ReplacedByToken = null,
            CreatedDate = DateTime.UtcNow,
            RevokedDate =null,
            RevokedByIp = "127.0.0.1",
        }
    };

    public static Mock<IUserTokenRepositoryAsync> GetUserRepositoryAsync()
    {
        var mockTokenRepositoryAsync = new Mock<IUserTokenRepositoryAsync>();

        // Setup GetByIdAsync
        mockTokenRepositoryAsync.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => MockTokens.FirstOrDefault(u => u.UserTokenId == id));

        // Setup GetByIdAsync
        mockTokenRepositoryAsync.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => MockTokens.FirstOrDefault(u => u.UserTokenId == id));

        // Setup GetAllAsync
        // mockTokenRepositoryAsync.Setup(repo => repo.GetAllAsync())
        //         .ReturnsAsync(MockTokens.AsReadOnly());

        // Setup AddAsync
        mockTokenRepositoryAsync.Setup(repo => repo.AddAsync(It.IsAny<UserToken>()))
                .ReturnsAsync((UserToken userToken) =>
                {
                    userToken.UserTokenId = Guid.NewGuid();
                    MockTokens.Add(userToken);
                    return userToken;
                });

        // Setup UpdateAsync
        mockTokenRepositoryAsync.Setup(repo => repo.UpdateAsync(It.IsAny<UserToken>()))
                .ReturnsAsync((UserToken userToken) =>
                {
                    var existingUserToken = MockTokens.FirstOrDefault(u => u.UserTokenId == userToken.UserTokenId);
                    if (existingUserToken == null) return null;
                    // Update fields as needed
                    existingUserToken.Token = userToken.Token;
                    existingUserToken.Expires = userToken.Expires;
                    existingUserToken.UserId = userToken.UserId;

                    return existingUserToken;
                });

        // Setup DeleteAsync
        mockTokenRepositoryAsync.Setup(repo => repo.DeleteAsync(It.IsAny<UserToken>()))
                .Callback<UserToken>(userToken => MockTokens.Remove(userToken));

        // Setup FindByCondition
        mockTokenRepositoryAsync.Setup(repo => repo.FindByCondition(It.IsAny<Expression<Func<UserToken, bool>>>()!))
                .ReturnsAsync((Expression<Func<UserToken, bool>> expression) =>
                {
                    var compiledExpression = expression.Compile();
                    return MockTokens.Where(compiledExpression).ToList().AsReadOnly();
                });

        return mockTokenRepositoryAsync;
    }
}