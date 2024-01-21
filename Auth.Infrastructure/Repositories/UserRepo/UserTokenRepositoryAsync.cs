using Auth.Application.Interfaces.Repositories;
using Auth.Core.Entities;
using Auth.Infrastructure.Context;

namespace Auth.Infrastructure.Repositories.UserRepo;

public class UserTokenRepositoryAsync : GenericRepositoryAsync<UserToken>, IUserTokenRepositoryAsync
{
    public UserTokenRepositoryAsync(DataContext dbContext) : base(dbContext)
    {
    }
}
