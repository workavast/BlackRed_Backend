using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary
{
    public class User
    {
        [Key] public int Id { get; set; }
        [Required] [MaxLength(256)] public string Name { get; set; }
        [Required] [MaxLength(256)] public string PasswordHash { get; set; }
        [Required] [MaxLength(256)] public string Salt { get; set; }

        public List<Level> Levels { get; set; }
        
        [InverseProperty("User1")] public virtual ICollection<FriendPair> FriendPairs1 { get; set; }
        [InverseProperty("User2")] public virtual ICollection<FriendPair> FriendPairs2 { get; set; }
        
        [InverseProperty("Sender")] public virtual ICollection<FriendRequest> Senders { get; set; }
        [InverseProperty("Recipient")] public virtual ICollection<FriendRequest> Recipients { get; set; }
    }
}