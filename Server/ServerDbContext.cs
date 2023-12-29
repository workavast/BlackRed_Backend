using Microsoft.EntityFrameworkCore;
using SharedLibrary;

namespace Server;

public class ServerDbContext : DbContext
{
    public ServerDbContext(DbContextOptions<ServerDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //this shit is need for work of two ForeignKey, BUT with this shit you possible get error, if delete User2, cos there DeleteBehavior.NoAction
        modelBuilder.Entity<FriendPair>()
            .HasOne(fp => fp.User2)
            .WithMany(u => u.FriendPairs2)
            .OnDelete(DeleteBehavior.NoAction);
        
        //this shit is need for work of two ForeignKey, BUT with this shit you possible get error, if delete Recipient, cos there DeleteBehavior.NoAction
        modelBuilder.Entity<FriendRequest>()
            .HasOne(fp => fp.Recipient)
            .WithMany(u => u.Recipients)
            .OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Level> Levels { get; set; }
    public DbSet<FriendPair> FriendPairs { get; set; }
    public DbSet<FriendRequest> FriendRequests { get; set; }
}