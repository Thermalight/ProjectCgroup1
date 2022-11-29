public class UserDto
{
    public Guid Guid { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsSubscribed { get; set; }
}