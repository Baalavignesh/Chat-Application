namespace Chat_Application.DTOs
{
    public class NewChatDto
    {
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string ReceiverName { get; set; }

        public bool IsOnline { get; set; }
    }
}
