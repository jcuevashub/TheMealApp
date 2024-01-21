using Auth.Application.Interfaces.Services;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Test.Mocks;

public static class MockPasswordService
{
    public static Mock<IPasswordService> GetMockPasswordService()
    {
        var mockPasswordService = new Mock<IPasswordService>();

        mockPasswordService.Setup(service => service.CreatePasswordHash(It.IsAny<string>(), out It.Ref<string>.IsAny, out It.Ref<string>.IsAny))
            .Callback((string password, out string hash, out string salt) =>
            {
                hash = "aBWaVgfOgzzmt/lYHV1lR2dVxGHdmc39AfIKtXSv2Lrm7Uer1Y29TvalIowb8VgJ1KGFODm4AKWCWRQD7TxUmQ==";
                salt = "YcWhryK1g1ZhXya3x3ufWtOsrrsCeyYCJJwWU/26ge3bxNPAYLcmruD8wI+MCzeWeRSiKjrcS1vyufFzFW6RL5Qzkau9OhfEjNlSIPTzdbLWXn2l0ifUPXsIhrmMcHJnujnY0XjlBQaIfWon3DgLs/qVLDfRtlBvCjJECOrd1Fk=";
            });

        mockPasswordService.Setup(service => service.VerifyPasswordHash(It.IsAny<string?>(), It.IsAny<byte[]>(), It.IsAny<byte[]>()))
            .Returns((string? password, byte[] storedHash, byte[] storedSalt) =>
            {
                IList<ValidationFailure> messages = new List<ValidationFailure>();

                if (password == null) messages.Add(new ValidationFailure("password", "Password is null"));
                if (string.IsNullOrWhiteSpace(password))
                    messages.Add(new ValidationFailure("password", "Value cannot be empty or whitespace only string."));
                if (storedHash.Length != 64)
                    messages.Add(new ValidationFailure("passwordHash", "Invalid length of password hash (64 bytes expected)."));
                if (storedSalt.Length != 128)
                    throw new ArgumentException("passwordHash", "Invalid length of password salt (128 bytes expected).");

                if (messages.Count > 0) throw new ValidationException(messages);

                using var hmac = new HMACSHA512(storedSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password!));

                return computedHash.SequenceEqual(storedHash);
            });

        mockPasswordService.Setup(service => service.Base64Decode(It.IsAny<string>()))
            .Returns((string s) => $"Decoded_{s}");

        mockPasswordService.Setup(service => service.Base64Decode(It.IsAny<string>()))
            .Returns((string s) => $"Decoded_{s}");

        mockPasswordService.Setup(service => service.GeneratePassword(It.IsAny<int>()))
            .Returns((int length) => new string('A', length)); // Returns a string with 'A' repeated 'length' times

        return mockPasswordService;
    }
}