using Auth.Core.Common;
using Microsoft.Extensions.Options;
using Moq;

namespace Auth.Test.Mocks;

public static class MockJwtSettings
{
    public static Mock<IOptions<JwtConfig>> GetMockJwtSettings()
    {

        Mock<IOptions<JwtConfig>> mockJwtSettings = new();

        var jwtConfig = new JwtConfig
        {
            Key = "C1CF4B7DC4C4175B6618DE4F55CA4ETheMealApp",
            Issuer = "TheMealApp",
            Audience = "TheMealApp",
            DurationInMinutes = 30,
            DurationInHours = 48,
            TokenLifetimeInMin = 30
        };

        mockJwtSettings.Setup(ap => ap.Value).Returns(jwtConfig);

        return mockJwtSettings;
    }
}