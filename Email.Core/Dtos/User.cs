namespace Email.Core.Dtos;
public class UserDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
}
