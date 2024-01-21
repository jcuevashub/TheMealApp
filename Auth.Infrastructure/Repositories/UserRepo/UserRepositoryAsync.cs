using Auth.Application.Interfaces.Repositories;
using Auth.Core.Entities;
using Auth.Infrastructure.Context;

namespace Auth.Infrastructure.Repositories.UserRepo;

public class UserRepositoryAsync : GenericRepositoryAsync<User>, IUserRepositoryAsync
{
    public UserRepositoryAsync(DataContext context) : base(context)
    {
    }

}
