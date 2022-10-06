using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ChengetaWebApp.Api.Database.Models;

public class Notification
{
    [Key]
    public Guid Guid { get; set; } = Guid.NewGuid();
    public ulong Time { get; set; }
    public int NodeId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    [JsonPropertyName("sound_type")]
    public string SoundType { get; set; }
    public int Probability { get; set; }
    public string Sound { get; set; }
    public bool Resolved { get; set; } = false;
}