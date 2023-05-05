using Chat_Application.DTOs;
using Chat_Application.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Chat_Application.Services
{
    public interface ISingleChatService
    {
        Task<ServiceResponse<IEnumerable<SingleChatMessage>>> GetSingleChat(int ChatId);
        Task<ServiceResponse<bool>> AddSingleChat(NewSingleChatDto request);
    }
    public class SingleChatService : ISingleChatService
    {
        private readonly ApplicationDbContext _context;

        // object to add a new data to the table
        public SingleChatService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<IEnumerable<SingleChatMessage>>> GetSingleChat(int ChatId)
        {
            var response = new ServiceResponse<IEnumerable<SingleChatMessage>>();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            response.Data = await _context.SingleChatMessage.Where(sc => sc.ParentChatId == ChatId).ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<bool>> AddSingleChat(NewSingleChatDto request)
        {
            var response = new ServiceResponse<bool>();

            var existingSingleChat = await _context.SingleChat.FirstOrDefaultAsync(x =>
                (x.ReceiverId == request.ReceiverId && x.SenderId == request.SenderId) ||
                (x.ReceiverId == request.SenderId && x.SenderId == request.ReceiverId));


            // Check if SingleChat table is having the receiver id if yes, just add data 
            if (existingSingleChat != null) {

                var newSingleChatMessage = new SingleChatMessage
                {
                    SenderId = request.SenderId,
                    ParentChatId = existingSingleChat.ChatId,
                    Message = request.Message,
                    Timestamp = DateTime.Now,
                    IsRead = false,
                };
                _context.SingleChatMessage.Add(newSingleChatMessage);
            }
            // New Chat User
            // new message, new user. Add to My Chats. Then SingleChat. Then SingleChatMessage

            else
            {
                var newSingleChat = new SingleChat
                {
                    SenderId = request.SenderId,
                    ReceiverId = request.ReceiverId
                };
                _context.SingleChat.Add(newSingleChat);

                await _context.SaveChangesAsync();

                var newSenderMyChat = new MyChats
                {
                    SingleChatId = newSingleChat.ChatId,
                    ChatUserId = request.SenderId,
                    GroupId = null
                };

                var newReceiverMyChat = new MyChats
                {
                    SingleChatId = newSingleChat.ChatId,
                    ChatUserId = request.ReceiverId,
                    GroupId = null
                };

                // Add the MyChats Table for both the receiver and the sender
                _context.MyChats.Add(newSenderMyChat);
                _context.MyChats.Add(newReceiverMyChat);


                await _context.SaveChangesAsync();
                var newSingleChatMessage = new SingleChatMessage
                {
                    SenderId = request.SenderId,
                    ParentChatId = newSingleChat.ChatId,
                    Message = request.Message,
                    Timestamp = DateTime.Now
                };
                _context.SingleChatMessage.Add(newSingleChatMessage);
                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();

            return response;

        }
    }
}
