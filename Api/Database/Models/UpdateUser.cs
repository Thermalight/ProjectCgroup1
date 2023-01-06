using System.ComponentModel.DataAnnotations;

namespace ChengetaWebApp.Api.Database.Models;

public class UpdateUser
{
    public Guid Guid { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public bool IsAdmin { get; set; }
}