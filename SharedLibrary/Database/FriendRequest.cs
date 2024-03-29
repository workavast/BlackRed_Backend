using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.Database
{
    public class FriendRequest
    {
        [Required] [Key] public int Id { get; set; }
        [Required] public int SenderId { get; set; }
        [Required] public int RecipientId { get; set; }
        
        [ForeignKey("SenderId")] public User Sender { get; set; }
        [ForeignKey("RecipientId")] public User Recipient { get; set; }
    }
}