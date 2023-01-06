using System.ComponentModel.DataAnnotations;

namespace ChengetaWebApp.Api.Database.Models;

public class User
{
    [Key]
    public Guid GUID { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
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
        if (updatedUser != null)
        {
            GUID = updatedUser.Guid;
            Username = updatedUser.Username;
            Password = updatedUser.Password;
            Email = updatedUser.Email;
            IsAdmin = updatedUser.IsAdmin;
        }
        
    }
}