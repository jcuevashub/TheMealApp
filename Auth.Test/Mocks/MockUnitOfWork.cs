using Auth.Application.Interfaces.Repositories;
using Moq;

namespace Auth.Test.Mocks;

public static class MockUnitOfWork
{
    public static Mock<IUnitOfWork> GetMockUnitOfWork()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork.Setup(uow => uow);


        return mockUnitOfWork;
    }
}