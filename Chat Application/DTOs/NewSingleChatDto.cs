using System.ComponentModel.DataAnnotations.Schema;

namespace Chat_Application.DTOs
{
    public class NewSingleChatDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
