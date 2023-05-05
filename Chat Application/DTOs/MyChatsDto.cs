﻿namespace Chat_Application.DTOs
{
    public class MyChatsDto
    {
        public int? SingleChatId { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string SenderName { get; set; } = string.Empty;

        public string ReceiverName { get; set; } = string.Empty;
        
        public bool isOnline { get; set; }
    }
}
