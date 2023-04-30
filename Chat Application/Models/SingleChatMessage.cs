using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat_Application.Models
{
    public class SingleChatMessage
    {
        [Key]
        public int MessageId { get; set; }

        [ForeignKey("SingleChat")]
        public int ParentChatId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int SenderId { get; set; }

        [Required]
        public string Message { get; set; } = string.Empty;

        [Required]
        public DateTime Timestamp { get; set; }

        public virtual User User { get; set; }
        public virtual SingleChat SingleChat { get; set; }

    }
}
