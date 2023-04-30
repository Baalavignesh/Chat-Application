using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat_Application.Models
{
    public class GroupChat
    {
        [Key]
        public int GroupId { get; set; }

        [Required]
        public string GroupName { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Admin")]
        public int AdminId { get; set; }


        public virtual User Admin { get; set; }  
        public virtual ICollection<MyChats> MyChats { get; set; }

        public virtual ICollection<GroupChatMessage> GroupChatMessage { get; set; }
        public virtual ICollection<GroupMembers> GroupMembers { get; set; }
    }
}
