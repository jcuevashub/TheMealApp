using Auth.Core.Common;

namespace Auth.Core.Entities;
public class User : BaseEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
}
