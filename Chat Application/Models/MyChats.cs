
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Chat_Application.Models
{
    public class MyChats
    {
        [Key]
        public int MyChatId { get; set; }

        [ForeignKey("SingleChat"), AllowNull]
        public int? SingleChatId { get; set; }

        [ForeignKey("GroupChat"), AllowNull]
        public int? GroupId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int ChatUserId { get; set; }

        public virtual User User { get; set; }

        public virtual SingleChat SingleChat { get; set; }

        public virtual GroupChat GroupChat { get; set; }

    }
}
