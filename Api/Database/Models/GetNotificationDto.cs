namespace ChengetaWebApp.Api.Database.Models
{
    public class GetNotificationsDto
    {
        public int? Limit { get; set; }
        public int? Probability { get; set; }
        public string? SoundType { get; set; }
    }
}