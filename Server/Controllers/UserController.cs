using Microsoft.AspNetCore.Mvc;
using SharedLibrary;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ServerDbContext _context;

    public UserController(ServerDbContext context)
    {
        _context = context;

        var user = new User()
        {
            Name = "name1",
            PasswordHash = "password",
            Salt = "salt"
        };

        _context.Add(user);
        _context.SaveChanges();
    }
    
    [HttpGet]
    public Guid Get([FromQuery] Guid guid)
    {
        
        return guid;
    }

    [HttpPost]
    public User Post(User user)
    {
        Console.WriteLine("User has been added");
    
        return user;
    }
}