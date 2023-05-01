using Chat_Application.DTOs;
using Chat_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chat_Application.Services
{

    public interface IUserInfoService
    {
        Task<ServiceResponse<IEnumerable<User>>> GetAllUser();
        Task<ServiceResponse<IEnumerable<MyChats>>> GetMyChats(int userId);
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

        public async Task<ServiceResponse<IEnumerable<MyChats>>> GetMyChats(int userId) {
            var response = new ServiceResponse<IEnumerable<MyChats>>();

            response.Data = await _context.MyChats
                .Where(mc => mc.ChatUserId == userId)
                .ToListAsync();

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
