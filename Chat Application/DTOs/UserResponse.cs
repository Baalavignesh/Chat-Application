using System.ComponentModel.DataAnnotations;

namespace Chat_Application.DTOs
{
    public class UserResponse
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;
    }
}
