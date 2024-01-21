namespace Catalog.Application.Responses;

public sealed record UserResponse
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string Email { get; set; }

    public string LastName { get; set; }

    public bool IsDeleted { get; set; }

}