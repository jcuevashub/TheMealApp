namespace Auth.Core.Entities;
public class UserToken
{
    public Guid UserTokenId { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public string JwtToken { get; set; }
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public string ReplacedByToken { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual DateTime? RevokedDate { get; set; }
    public string RevokedByIp { get; set; }
    public bool IsActive => RevokedDate == null && !IsExpired;
}
