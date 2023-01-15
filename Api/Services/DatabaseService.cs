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
        await dbContext.AddAsync(new SoundPriority(){SoundType="thunder", Priority=2});
        await dbContext.AddAsync(new SoundPriority(){SoundType="vehicle", Priority=3});
        await dbContext.AddAsync(new SoundPriority(){SoundType="animal", Priority=4});
        await dbContext.AddAsync(new SoundPriority(){SoundType="unknown", Priority=5});


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

    public IEnumerable<Notification> GetAllNotifications(GetNotificationsDto dto)
    {
        var DbContext = new SqliteDbContext();
        var queriable = DbContext.Notifications.AsQueryable();

        if (dto.Probability != null)
            queriable = queriable.Where(n => n.Probability >= dto.Probability);
        if (dto.SoundType != null)
            queriable = queriable.Where(n => n.SoundType == dto.SoundType);
        

        var notifications = queriable.ToList();

        IEnumerable<Notification> sorted = notifications.OrderByDescending(x => x.Time);
        if (dto.Limit < notifications.Count() && dto.Limit != null && dto.Limit > 0)
            sorted = sorted.Take((int)dto.Limit);
        
        return sorted;
    }

    public IResult UpdateNotificationStatus(Guid id, int status) {
        var DbContext = new SqliteDbContext();
        var notification = DbContext.Notifications.Find(id);
        if (notification == null)
            return Results.NotFound();

        notification.StatusID = status;
        DbContext.SaveChanges();

        return Results.NoContent();
    }

    public bool AddUser(GetUser newUser)
    {
        var dbContext = new SqliteDbContext();
        User User = new(newUser);
        // check if user already exists
        if (dbContext.Users.Where(u => u.Email == User.Email).FirstOrDefault() != null)
            return false;
        // hash the given password
        User.Password = Bcrypt.HashPassword(User.Password);
        // add the user to the database and save
        dbContext.Add(User);
        var result = dbContext.SaveChanges();

        if (result == 1)
            return true;
        return false;
    }

    public bool DeleteUser(Guid userGuid)
    {
        SqliteDbContext dbContext = new SqliteDbContext();
        User? User = dbContext.Users.Where(u => u.GUID == userGuid).FirstOrDefault();
        if (User == null)
            return false;
        dbContext.Remove(User);
        dbContext.SaveChanges();
        return true;
    }

    public bool UpdateUser(UpdateUser updatedUser)
    {
        var dbContext = new SqliteDbContext();

        User UserUpdated = new User(updatedUser);
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
        var result = dbContext.SaveChanges();

        return true;
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

    public User? VerifyUserLogin(string email, string password)
    {
        var DbContext = new SqliteDbContext();
        var user = DbContext.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        if (user != null && Bcrypt.Verify(password, user.Password))
            return new User()
            {
                GUID = user.GUID,
                Username = user.Username,
                Email = user.Email.ToLower(),
                IsAdmin = user.IsAdmin
            };
        return null;
    }
}