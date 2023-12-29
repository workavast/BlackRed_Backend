using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.Database
{
    public class FriendPair
    {
        [Required] [Key] public int Id { get; set; }
        [Required] public int User1Id { get; set; }
        [Required] public int User2Id { get; set; }

        [ForeignKey("User1Id")] public User User1 { get; set; }
        [ForeignKey("User2Id")] public User User2 { get; set; }
    }
}