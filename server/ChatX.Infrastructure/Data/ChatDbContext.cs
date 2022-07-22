using ChatX.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChatX.Infrastructure.Data;

public class ChatDbContext : DbContext
{
    public DbSet<Conversation> Conversations => Set<Conversation>();
    public DbSet<User> Users => Set<User>();


    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedNever();
        modelBuilder.Entity<User>().HasIndex(u => u.SearchStartedAt);

        modelBuilder.Entity<Conversation>().Property(c => c.Id).ValueGeneratedNever();
        modelBuilder.Entity<Conversation>().HasIndex(c => new { c.UserOneId, c.UserTwoId }).IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}