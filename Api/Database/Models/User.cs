using System.ComponentModel.DataAnnotations;

namespace ChengetaWebApp.Api.Database.Models;

public class User
{
    [Key]
    public Guid GUID { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; } = false;
    public Subscriber? Subscriber { get; set; } = null;

    public User() {}
    public User(GetUser newUser)
    {
        GUID = Guid.NewGuid();
        Username = newUser.Username;
        Password = newUser.Password;
        Email = newUser.Email;
        IsAdmin = newUser.IsAdmin;
    }

    public User(UpdateUser updatedUser)
    {
        GUID = updatedUser.GUID;
        Username = updatedUser.Username;
        Password = updatedUser.Password;
        Email = updatedUser.Email;
        IsAdmin = updatedUser.IsAdmin;
    }
}