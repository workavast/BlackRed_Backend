using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Server.Models;
using SharedLibrary;
using SharedLibrary.Database;

namespace Server.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ServerDbContext _dbContext;
    private readonly Settings _settings;

    public AuthenticationService(ServerDbContext dbContext, Settings settings)
    {
        _dbContext = dbContext;
        _settings = settings;
    }

    public (bool valide, string content) Register(string login, string password)
    {
        if (_dbContext.Users.Any(s => s.Name == login))
            return (false, ErrorType.UserWithTisNameIsExist.ToString());

        var passwordHash = ComputeHash(password, login);
        var user = new User{Name = login, PasswordHash = passwordHash};

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        
        return (true, ErrorType.None.ToString());
    }
    
    public (bool valide, string content) Login(string login, string password)
    {
        var user = _dbContext.Users.SingleOrDefault(u => u.Name == login);
        
        if (user is null)
            return (false, ErrorType.InvalidLogin.ToString());

        if (user.PasswordHash != ComputeHash(password, login))
            return (false, ErrorType.InvalidPassword.ToString());

        return (true, GenerateJwtToken(CreateIdentity(user)));
    }

    private string ComputeHash(string password, string saltString)
    {
        // var salt = Convert.FromBase64String(saltString);
        //
        // using var hashGenerator = new Rfc2898DeriveBytes(password, salt);
        //
        // hashGenerator.IterationCount = 10101;
        // var bytes = hashGenerator.GetBytes(24);
        //
        // return Convert.ToBase64String(bytes);

        using var sah = SHA512.Create();
        var bytes = sah.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private ClaimsIdentity CreateIdentity(User user) 
        => new(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) });
    
    private string GenerateJwtToken(ClaimsIdentity identity)
    {
        var key = Encoding.UTF8.GetBytes(_settings.BearerKey);

        var tokenDescription = new SecurityTokenDescriptor()
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddYears(10),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);

        return tokenHandler.WriteToken(token);
    }
}

public interface IAuthenticationService
{
    public (bool valide, string content) Register(string login, string password);
    public (bool valide, string content) Login(string login, string password);
}