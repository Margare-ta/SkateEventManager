using Microsoft.EntityFrameworkCore;
using SkateEventManager.Models;

namespace SkateEventManager;

public class DatabaseContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Book> Rent { get; set; }
    public DbSet<Skate> Skates { get; set; }

    //no password
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            "server=localhost;database=skateeventdb;user=root;password=;",
            new MySqlServerVersion(new Version(8, 0, 32))
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Book → User Relationship
        modelBuilder.Entity<Book>()
            .HasOne(b => b.User)
            .WithMany(u => u.Books)
            .HasForeignKey(b => b.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        // Book → Event Relationship
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Event)
            .WithMany(e => e.Books)
            .HasForeignKey(b => b.EventID)
            .OnDelete(DeleteBehavior.Cascade);

        // Book → Skate Relationship
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Skate)
            .WithMany(s => s.Books)
            .HasForeignKey(b => b.SkateID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
