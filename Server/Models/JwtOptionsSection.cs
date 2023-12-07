namespace Server.Models;

public class JwtOptionsSection
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}