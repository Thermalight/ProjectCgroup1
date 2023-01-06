using System.ComponentModel.DataAnnotations;

namespace ChengetaWebApp.Api.Database.Models;

public class SoundPriority
{
    [Key]
    public string? SoundType { get; set; }
    public int Priority { get; set; } 
    public List<Notification>? Notifications { get; set; }
    public List<Subscriber>? Subscribers { get; set; }
}