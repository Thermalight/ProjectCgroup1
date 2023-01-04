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
            return;

        using SqliteDbContext dbContext = new SqliteDbContext();
        using var transaction = dbContext.Database.BeginTransaction();
        await dbContext.Database.EnsureCreatedAsync();

        // create all priorities
        await dbContext.AddAsync(new SoundPriority(){SoundType="gunshot", Priority=1});
        await dbContext.AddAsync(new SoundPriority(){SoundType="vehicle", Priority=2});
        await dbContext.AddAsync(new SoundPriority(){SoundType="animal", Priority=3});
        await dbContext.AddAsync(new SoundPriority(){SoundType="unknown", Priority=4});

        // create all statustypes
        await dbContext.AddAsync(new Status(){Description="The notification is not being handled yet", Name="Not handled"});
        await dbContext.AddAsync(new Status(){Description="A ranger is checking out the event", Name="Being handled"});
        await dbContext.AddAsync(new Status(){Description="The event has been closed", Name="Handled"});
        await dbContext.AddAsync(new Status(){Description="The notification was not what it appeared to be", Name="False alarm"});

        // create admin account
        await dbContext.AddAsync(new User(){Username="Admin", Password=Bcrypt.HashPassword("Password123!"), IsAdmin=true, Email="debug@debug.com"});
        await dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public async Task AddNotificationAsync(Notification notification)
    {
        var DbContext = new SqliteDbContext();
        await DbContext.AddAsync(notification);
        await DbContext.SaveChangesAsync();
    }

    public IEnumerable<Notification> GetAllNotifications(int limit = -1)
    {
        var DbContext = new SqliteDbContext();
        var notifications = DbContext.Notifications.ToList();

        IEnumerable<Notification> sorted = notifications.OrderByDescending(x => x.Time);
        if (limit < notifications.Count() && limit > 0)
            sorted = sorted.Take(limit);
        
        return sorted;
    }

    public bool UserAuthentication(string FunctionUsername, string FunctionPassword, HttpContext context)
    {
        var DbContext = new SqliteDbContext();
        var user = (from item in DbContext.Users
                    where item.Email == FunctionUsername
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

    public async Task<IResult> UpdateNotificationStatusAsync(Guid id, int status) {
        var DbContext = new SqliteDbContext();
        var notification = await DbContext.Notifications.FindAsync(id);
        if (notification == null)
            return Results.NotFound();

        notification.StatusID = status;
        await DbContext.SaveChangesAsync();

        return Results.NoContent();
    }

    public async Task<bool> AddUser(GetUser newUser)
    {
        var dbContext = new SqliteDbContext();
        User User = new(newUser);
        User.Password = Bcrypt.HashPassword(User.Password);
        dbContext.Add(User);
        var result = await dbContext.SaveChangesAsync();

        if (result == 1)
            return true;
        return false;
    }

    public async Task<bool> DeleteUser(Guid userGuid)
    {
        var dbContext = new SqliteDbContext();
        User? User = dbContext.Users.Where(u => u.GUID == userGuid).FirstOrDefault();
        if (User == null)
            return false;
        dbContext.Remove(User);
        dbContext.SaveChanges();
        return true;
    }

    public async Task<bool> UpdateUser(UpdateUser updatedUser)
    {
        var dbContext = new SqliteDbContext();

        User UserUpdated = new(updatedUser);
        User targetUser = dbContext.Users.Single(user => user.GUID == UserUpdated.GUID);

        if (targetUser == null)
            return false;

        if (string.IsNullOrEmpty(updatedUser.Username))
            updatedUser.Username = targetUser.Username;

        if (string.IsNullOrEmpty(updatedUser.Password)){
            updatedUser.Password = targetUser.Password;
        }

        else {
            updatedUser.Password = Bcrypt.HashPassword(updatedUser.Password);
        }

        if (string.IsNullOrEmpty(updatedUser.Email))
            updatedUser.Email = targetUser.Email;

        dbContext.Entry(targetUser).CurrentValues.SetValues(updatedUser);
        var result = await dbContext.SaveChangesAsync();

        if (result == 1)
            return true;
        return false;
    }

    public List<UserDto> GetAllUsers()
    {
        var DbContext = new SqliteDbContext();
        var queriable = DbContext.Users.AsQueryable();

        var users = queriable.Select(u => new UserDto() {
            Username = u.Username,
            Email = u.Email,
            IsAdmin = u.IsAdmin,
            Guid = u.GUID,
            IsSubscribed = u.Subscriber != null
        }).ToList();

        return users;
    }

    public User VerifyUserLogin(string email, string password)
    {
        var DbContext = new SqliteDbContext();
        var user = DbContext.Users.FirstOrDefault(u => u.Email == email);
        if (user != null && Bcrypt.Verify(password, user.Password))
            return new User()
            {
                GUID = user.GUID,
                Username = user.Username,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };
        return null;
    }
}