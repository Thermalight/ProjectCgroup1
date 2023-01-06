public class UserDto
{
    public Guid Guid { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsAdmin { get; set; }
    public bool IsSubscribed { get; set; }
}