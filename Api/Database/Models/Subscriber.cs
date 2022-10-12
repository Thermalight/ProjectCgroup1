using System.ComponentModel.DataAnnotations;

namespace ChengetaWebApp.Api.Database.Models;

public class Subscriber
{
    [Key]
    public int ID { get; set; }
    public Guid UserID { get; set; }
    public User User { get; set; }
    public string MinimumPriority { get; set; } = "unknown";
    public SoundPriority Priority { get; set; }
}