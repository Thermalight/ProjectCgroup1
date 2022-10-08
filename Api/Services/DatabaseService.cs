using ChengetaWebApp.Api.Database;
using ChengetaWebApp.Api.Database.Models;

namespace ChengetaWebApp.Api.Services;

public class DatabaseService
{
    private string DBLocation = "./Database.db";

    public async void Setup()
    {
        if(File.Exists(DBLocation))
            File.Delete(DBLocation);

        using var dbContext = new SqliteDbContext();
        await dbContext.Database.EnsureCreatedAsync();
        await dbContext.SaveChangesAsync();
    }

    public async Task AddNotificationAsync(Notification notification)
    {
        await using var DbContext = new SqliteDbContext();
        await DbContext.AddAsync(notification);
        await DbContext.SaveChangesAsync();
    }

    public List<Notification> GetAllNotifications()
    {
        using var DbContext = new SqliteDbContext();
        var notifications = DbContext.Notifications.ToList();
        notifications.Reverse();
        return notifications;
    }
}