using ChengetaWebApp.Api.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ChengetaWebApp.Api.Database
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Subscriber> Subscribers { get; set; } = null!;
        public DbSet<SoundPriority> Priorities { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Priority)
                .WithMany(p => p.Notifications)
                .HasForeignKey(n => n.SoundType);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Status)
                .WithMany(p => p.Notifications)
                .HasForeignKey(n => n.StatusID);

            modelBuilder.Entity<Subscriber>()
                .HasOne(s => s.User)
                .WithOne(u => u.Subscriber)
                .HasForeignKey<Subscriber>(s => s.UserID);

            modelBuilder.Entity<Subscriber>()
                .HasOne(s => s.Priority)
                .WithMany(p => p.Subscribers)
                .HasForeignKey(s => s.MinimumPriority);
        }
        
        public static SqliteDbContext Create()
            => new();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source=Database.db");
    }
}