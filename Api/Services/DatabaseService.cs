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

        using SqliteDbContext dbContext = new SqliteDbContext();
        using var transaction = dbContext.Database.BeginTransaction();
        await dbContext.Database.EnsureCreatedAsync();

        // create all priorities
        await dbContext.AddAsync(new SoundPriority(){SoundType="gunshot", Priority=1});
        await dbContext.AddAsync(new SoundPriority(){SoundType="vehicle", Priority=2});
        await dbContext.AddAsync(new SoundPriority(){SoundType="animal", Priority=3});
        await dbContext.AddAsync(new SoundPriority(){SoundType="unknown", Priority=4});

        // create all statustypes
        await dbContext.AddAsync(new Status(){Description="A ranger is checking out the event", Name="Being handled"});
        await dbContext.AddAsync(new Status(){Description="The notification is not being handled yet", Name="Not handled"});
        await dbContext.AddAsync(new Status(){Description="The event has been closed", Name="Handled"});
        await dbContext.AddAsync(new Status(){Description="The notification was not what it appeared to be", Name="False alarm"});

        // create admin account
        await dbContext.AddAsync(new User(){Username="Admin", Password=Bcrypt.HashPassword("Password123!"), IsAdmin=true, Email="debug@debug.com"});
        await dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
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
    public bool UserAuthentication(string FunctionUsername, string FunctionPassword, HttpContext context)
    {
        using var DbContext = new SqliteDbContext();
        var user = (from item in DbContext.Users
                    where item.Username == FunctionUsername
                    select item).FirstOrDefault();
        if (user != null && Bcrypt.Verify(FunctionPassword,user.Password))
        {
            context.Response.Cookies.Append("Username",FunctionUsername);
            context.Response.Cookies.Append("Admin",user.IsAdmin.ToString());
            context.Response.Cookies.Append("LoggedIn","True");
            return true;
        }
        context.Response.Cookies.Append("LoggedIn","False");
        
        return false;
    }
}