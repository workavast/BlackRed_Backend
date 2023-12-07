using System.ComponentModel.DataAnnotations;

namespace SharedLibrary;

public class User
{
    public int Id { get; set; }
    [Required] [MaxLength(256)] public string Name { get; set; }
    [Required] [MaxLength(256)] public string PasswordHash { get; set; }
    [Required] [MaxLength(256)] public string Salt { get; set; }

    public List<Level> Levels { get; set; }
}
