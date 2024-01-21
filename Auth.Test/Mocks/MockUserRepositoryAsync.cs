using Auth.Application.Interfaces.Repositories;
using Auth.Core.Entities;
using Moq;
using System.Linq.Expressions;

namespace Auth.Test.Mocks;

public static class MockUserRepositoryAsync
{
    private static readonly List<User> MockUsers = new List<User>
    {
        new User
        {
            Id = new Guid("f6532788-f64b-4c1c-8084-f3212962a88f"),
            FirstName = "User1",
            LastName = "User1",
            Email = "user1@example.com",
            PasswordHash = "aBWaVgfOgzzmt/lYHV1lR2dVxGHdmc39AfIKtXSv2Lrm7Uer1Y29TvalIowb8VgJ1KGFODm4AKWCWRQD7TxUmQ==",
            PasswordSalt = "YcWhryK1g1ZhXya3x3ufWtOsrrsCeyYCJJwWU/26ge3bxNPAYLcmruD8wI+MCzeWeRSiKjrcS1vyufFzFW6RL5Qzkau9OhfEjNlSIPTzdbLWXn2l0ifUPXsIhrmMcHJnujnY0XjlBQaIfWon3DgLs/qVLDfRtlBvCjJECOrd1Fk=",
        },
        new User
        {
            Id = new Guid("f6532788-f64b-4c1c-8084-f3212962a883"),
            FirstName = "User1",
            LastName = "User1",
            Email = "user1@example.com",
            PasswordHash = "aBWaVgfOgzzmt/lYHV1lR2dVxGHdmc39AfIKtXSv2Lrm7Uer1Y29TvalIowb8VgJ1KGFODm4AKWCWRQD7TxUmQ==",
            PasswordSalt = "YcWhryK1g1ZhXya3x3ufWtOsrrsCeyYCJJwWU/26ge3bxNPAYLcmruD8wI+MCzeWeRSiKjrcS1vyufFzFW6RL5Qzkau9OhfEjNlSIPTzdbLWXn2l0ifUPXsIhrmMcHJnujnY0XjlBQaIfWon3DgLs/qVLDfRtlBvCjJECOrd1Fk=",
            IsDeleted = false,
            UpdatedDate = null,
            DeletedDate = null
        },
        new User
        {
            Id = new Guid("f6532788-f64b-4c1c-8084-f3212962a883"),
            FirstName = "User1",
            LastName = "User1",
            Email = "social@example.com",
            PasswordHash = "aBWaVgfOgzzmt/lYHV1lR2dVxGHdmc39AfIKtXSv2Lrm7Uer1Y29TvalIowb8VgJ1KGFODm4AKWCWRQD7TxUmQ==",
            PasswordSalt = "YcWhryK1g1ZhXya3x3ufWtOsrrsCeyYCJJwWU/26ge3bxNPAYLcmruD8wI+MCzeWeRSiKjrcS1vyufFzFW6RL5Qzkau9OhfEjNlSIPTzdbLWXn2l0ifUPXsIhrmMcHJnujnY0XjlBQaIfWon3DgLs/qVLDfRtlBvCjJECOrd1Fk=",
            IsDeleted = false,
            UpdatedDate = null,
            DeletedDate = null
        },
        new User
        {
        Id = new Guid("f6532788-f64b-4c1c-8084-f3212962a887"),
        FirstName = "User1",
        LastName = "User1",
        Email = "current@example.com",
        PasswordHash = "aBWaVgfOgzzmt/lYHV1lR2dVxGHdmc39AfIKtXSv2Lrm7Uer1Y29TvalIowb8VgJ1KGFODm4AKWCWRQD7TxUmQ==",
        PasswordSalt = "YcWhryK1g1ZhXya3x3ufWtOsrrsCeyYCJJwWU/26ge3bxNPAYLcmruD8wI+MCzeWeRSiKjrcS1vyufFzFW6RL5Qzkau9OhfEjNlSIPTzdbLWXn2l0ifUPXsIhrmMcHJnujnY0XjlBQaIfWon3DgLs/qVLDfRtlBvCjJECOrd1Fk=",
        IsDeleted = false,
        UpdatedDate = null,
        DeletedDate = null
    }
    };

    public static Mock<IUserRepositoryAsync> GetUserRepositoryAsync()
    {
        var mockRepo = new Mock<IUserRepositoryAsync>();

        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => MockUsers.FirstOrDefault(u => u.Id == id));

        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => MockUsers.FirstOrDefault(u => u.Id == id));

        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => MockUsers.FirstOrDefault(u => u.Id == id));

        mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(MockUsers.AsReadOnly());

        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .ReturnsAsync((User user) =>
                {
                    user.Id = Guid.NewGuid();
                    MockUsers.Add(user);
                    var newUser = MockUsers.FirstOrDefault(u => u.Id == user.Id);
                    return newUser;
                });

        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync((User user) =>
                {
                    var existingUser = MockUsers.FirstOrDefault(u => u.Id == user.Id);
                    if (existingUser == null) return null;
                    existingUser.FirstName = user.FirstName;
                    existingUser.LastName = user.LastName;
                    existingUser.Email = user.Email;

                    return existingUser;
                });

        mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<User>()))
                .Callback<User>(user => MockUsers.Remove(user));

        mockRepo.Setup(repo => repo.FindByCondition(It.IsAny<Expression<Func<User, bool>>>()!))
                .ReturnsAsync((Expression<Func<User, bool>> expression) =>
                {
                    var compiledExpression = expression.Compile();
                    return MockUsers.Where(compiledExpression).ToList().AsReadOnly();
                });

        return mockRepo;
    }
}