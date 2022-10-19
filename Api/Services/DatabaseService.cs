using ChengetaWebApp.Api.Database;
using ChengetaWebApp.Api.Database.Models;
using Bcrypt = BCrypt.Net.BCrypt;

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

        // create all priorities
        await dbContext.AddAsync(new SoundPriority(){SoundType="gunshot", Priority=1});
        await dbContext.AddAsync(new SoundPriority(){SoundType="vehicle", Priority=2});
        await dbContext.AddAsync(new SoundPriority(){SoundType="animal", Priority=3});
        await dbContext.AddAsync(new SoundPriority(){SoundType="unknown", Priority=4});
        await dbContext.SaveChangesAsync();

        // create all statustypes
        await dbContext.AddAsync(new Status(){Description="The notification is not being handled yet", Name="Not handled"});
        await dbContext.AddAsync(new Status(){Description="A ranger is checking out the event", Name="Being handled"});
        await dbContext.AddAsync(new Status(){Description="The event has been closed", Name="Handled"});
        await dbContext.AddAsync(new Status(){Description="The notification was not what it appeared to be", Name="False alarm"});
        await dbContext.SaveChangesAsync();

        // create admin account
        await dbContext.AddAsync(new User(){Username="Admin", Password=Bcrypt.HashPassword("Password123!"), IsAdmin=true, Email="debug@debug.com"});
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

    public async Task<IResult> UpdateNotificationStatusAsync(Guid id, int status) {
        var DbContext = new SqliteDbContext();
        var notification = await DbContext.Notifications.FindAsync(id);
        if (notification == null)
            return Results.NotFound();

        notification.StatusID = status;
        await DbContext.SaveChangesAsync();

        return Results.NoContent();
    }
}