using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat_Application.Models
{
    public class SingleChat
    {
        [Key]
        public int ChatId { get; set; }

        [Required]
        [ForeignKey("Sender")]
        public int SenderId { get; set; }

        [Required]
        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }


        public virtual ICollection<MyChats> MyChats { get; set; }

        public virtual ICollection<SingleChatMessage> SingleChatMessages { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }

    }
}
