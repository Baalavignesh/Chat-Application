using Chat_Application.DTOs;
using Chat_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chat_Application.Services
{
    public interface IUserService
    {
        Task<User> RegisterUser(UserDto request);
        Task<User> CheckUser(UserDto request);
    }
    public class UserService : IUserService
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<User> CheckUser(UserDto request)
        {
            if (_context.User == null)
            {
                return null;
            }
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<User> RegisterUser(UserDto request)
        {
            if (_context.User == null)
            {
                return null;
            }
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                return null;
            }

            return user;
        }




    }
}
