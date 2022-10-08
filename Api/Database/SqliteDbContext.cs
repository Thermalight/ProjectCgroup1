using ChengetaWebApp.Api.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ChengetaWebApp.Api.Database
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<Notification> Notifications { get; set; }

        public static SqliteDbContext Create()
            => new();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source=Database.db");
    }
}