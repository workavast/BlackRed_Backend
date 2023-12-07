using Microsoft.EntityFrameworkCore;
using SharedLibrary;

namespace Server;

public class ServerDbContext : DbContext
{
    public ServerDbContext(DbContextOptions<ServerDbContext> options) 
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Level> Levels { get; set; }
}