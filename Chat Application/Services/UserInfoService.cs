using Chat_Application.DTOs;
using Chat_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chat_Application.Services
{

    public interface IUserInfoService
    {
        Task<ServiceResponse<IEnumerable<User>>> GetAllUser();
        Task<ServiceResponse<IEnumerable<MyChatsDto>>> GetMyChats(int userId);

        Task<ServiceResponse<IEnumerable<NewChatDto>>> GetSpecificUser(string username, int userId);

    }
    public class UserInfoService : IUserInfoService
    {
        private readonly ApplicationDbContext _context;
        public UserInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<IEnumerable<User>>> GetAllUser()
        {
            var response = new ServiceResponse<IEnumerable<User>>();
            if (_context.User == null)
            {
                response.Error = "No User Found";
            }
            response.Data = await _context.User.ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<MyChatsDto>>> GetMyChats(int userId)
        {
            var response = new ServiceResponse<IEnumerable<MyChatsDto>>();
            Console.WriteLine(userId);
            response.Data = await _context.MyChats
                .Where(c => c.ChatUserId == userId && c.SingleChatId != null)
                .Include(c => c.SingleChat.Sender)
                .Include(c => c.SingleChat.Receiver) 
                .Include(c => c.User)
                .Select(c => new MyChatsDto
                {
                    SingleChatId = c.SingleChatId,
                    SenderId = c.SingleChat.SenderId == userId ? c.SingleChat.SenderId : c.SingleChat.ReceiverId,
                    ReceiverId = c.SingleChat.SenderId == userId ? c.SingleChat.ReceiverId : c.SingleChat.SenderId,
                    SenderName = c.SingleChat.SenderId == userId ? c.SingleChat.Sender.Username : c.SingleChat.Receiver.Username, 
                    ReceiverName = c.SingleChat.SenderId == userId ? c.SingleChat.Receiver.Username : c.SingleChat.Sender.Username,
                    isOnline = c.SingleChat.SenderId != userId
                ? _context.User.Any(u => u.UserId == c.SingleChat.SenderId && u.isOnline)
                : _context.User.Any(u => u.UserId == c.SingleChat.ReceiverId && u.isOnline)
                })
                .ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<NewChatDto>>> GetSpecificUser(string username, int userId)
        {
            var response = new ServiceResponse<IEnumerable<NewChatDto>>();
            if (_context.User == null)
            {
                response.Error = "No User Found";
            }
            var users = await _context.User.Where(u => u.Username.StartsWith(username) && u.UserId != userId).ToListAsync();

            var NewChatDtos = users.Select(u => new NewChatDto
            {
               SenderId = userId,
               ReceiverId = u.UserId,
                ReceiverName = u.Username,
                IsOnline = u.isOnline

            });
            response.Data = NewChatDtos;
            return response;

        }

        // To delete a chat

        //public async Task<IActionResult> DeleteMyChats(int id)
        //{
        //    if (_context.MyChats == null)
        //    {
        //        return NotFound();
        //    }
        //    var myChats = await _context.MyChats.FindAsync(id);
        //    if (myChats == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.MyChats.Remove(myChats);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

    }
}
