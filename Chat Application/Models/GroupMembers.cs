using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat_Application.Models
{
    public class GroupMembers
    {
        [Key]
        public int MemberId { get; set; }

        [Required]
        [ForeignKey("GroupChat")]
        public int GroupId { get; set; }

        [Required]
        [ForeignKey("GroupMember")]
        public int UserId { get; set; }

        public virtual GroupChat GroupChat { get; set; }

        public virtual User GroupMember { get; set; }

    }
}
