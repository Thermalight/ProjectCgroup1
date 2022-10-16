using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChengetaWebApp.Api.Database.Models;

public class Status
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<Notification>? Notifications { get; set; }
}