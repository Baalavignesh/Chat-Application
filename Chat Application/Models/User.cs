using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Chat_Application.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [JsonIgnore]
        public byte[] PasswordHash { get; set; } 

        [Required]
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public bool isOnline { get; set; }  

        public virtual ICollection<MyChats> MyChats { get; set; }
        public virtual ICollection<SingleChat> SingleChat { get; set; }
        public virtual ICollection<SingleChatMessage> SingleChatMessage { get; set; }
        public virtual ICollection<GroupChatMessage> GroupChatMessage { get; set; }
        public virtual ICollection<GroupChat> GroupChat { get; set; }
        public virtual ICollection<GroupMembers> GroupMembers { get; set; }
    }
}
