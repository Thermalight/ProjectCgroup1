namespace ChengetaWebApp.Api.Database.Models;

public class GetUser
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; } = false;
}