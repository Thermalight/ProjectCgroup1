using System.ComponentModel.DataAnnotations;

namespace ChengetaWebApp.Api.Database.Models;

public class Subscriber
{
    [Key]
    public int ID { get; set; }
    public Guid UserID { get; set; }
    public User User { get; set; }
    public int MinimumPriority { get; set; } = 0;
}