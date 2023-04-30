using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Chat_Application.Models
{
    public class GroupChatMessage
    {
        [Key]
        public int MessageId { get; set; }

        [ForeignKey("GroupChat")]
        public int GroupId { get; set; }

        [Required]
        [ForeignKey("Sender")]
        public int GroupSenderId { get; set; }

        [Required]
        public string Message { get; set; } = string.Empty;

        [Required]
        public DateTime Timestamp { get; set; }

        public virtual User Sender { get; set; }
        public virtual GroupChat GroupChat { get; set; }
    }
}
