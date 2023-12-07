using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary;

public class Level
{
    [Key] public int Id { get; set; }
    [Required] [ForeignKey("UserId")] public int UserId { get; set; }
    [Required] public int Num { get; set; }
    [Required] public float Time { get; set; }
    [Required] [MaxLength(Int32.MaxValue)] public string Way { get; set; }

    public User User { get; set; }
}