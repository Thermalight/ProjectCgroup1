namespace ChengetaWebApp.Api.Database.Models;

public class UpdateUser
{
    public Guid GUID { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsAdmin { get; set; }
}