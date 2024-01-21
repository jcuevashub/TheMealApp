using Auth.Application.Handlers;
using Auth.Application.Queries;
using Auth.Test.Mocks;
using Shouldly;

namespace Auth.Test.Features.Queries;

public class GetUserHandlerTest
{
    private readonly GetUserHandler _getUserHandler;

    public GetUserHandlerTest()
    {
        var userRepositoryAsync = MockUserRepositoryAsync.GetUserRepositoryAsync();
        var mockMapper = MockMapper.GetMockMapper();

        _getUserHandler = new GetUserHandler(
            userRepositoryAsync.Object,
            mockMapper.Object
            );
    }

    [Fact]
    public async Task Get_Current_User_Success()
    {
        var getUserRequest = new GetUserRequest(Id: new Guid("f6532788-f64b-4c1c-8084-f3212962a887"));

        //Act
        var response = await _getUserHandler.Handle(getUserRequest, CancellationToken.None);

        //Assets
        response.Status.ShouldBe(true);
    }

    [Fact]
    public async Task Get_Current_User_Fails()
    {
        var getUserRequest = new GetUserRequest(Id: new Guid("f6532788-f64b-4c1c-8084-f3212962a82f"));

        //Act
        var response = await _getUserHandler.Handle(getUserRequest, CancellationToken.None);

        //Assets
        response.Message.ShouldBe("User not found or Deleted.");
    }

}