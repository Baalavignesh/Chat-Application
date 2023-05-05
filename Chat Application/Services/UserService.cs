using Chat_Application.DTOs;
using Chat_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Chat_Application.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<User>> CheckUser(UserDto request);
        Task<ServiceResponse<User>> RegisterUser(string username, byte[] PasswordHash, byte[] passwordSalt);
        Task<ServiceResponse<User>> LoginUser(UserDto request);

    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<ServiceResponse<User>> CheckUser(UserDto request)
        {
            var response = new ServiceResponse<User>();
            if (_context.User == null)
            {
                response.Error = "Db Server Error";
            }
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == request.Username);
            response.Data = user;
            response.Error = "User Already Exists";

            if (user == null)
            {
                response.Error = "User not found";
            }
            return response;
        }

        public async Task<ServiceResponse<User>> RegisterUser(string username, byte[] PasswordHash,  byte[] passwordSalt)
        {
            User NewUser = new User()
            {
                Username = username,
                PasswordHash = PasswordHash,
                PasswordSalt = passwordSalt,
                isOnline = false
            };



            var response = new ServiceResponse<User>();
            if (_context.User == null)
            {
                response.Error = "Db Server Error";
            }
            _context.User.Add(NewUser);
            
            await _context.SaveChangesAsync();
            response.Data = NewUser;
            return response;
        }


        public async Task<ServiceResponse<User>> LoginUser(UserDto request)
        {
            var response = new ServiceResponse<User>();

            if (_context.User == null)
            {
                response.Error = "Db Server Error";
            }
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                response.Error = "User not found";
            }
            else
            {
                response.Data = user;
            }
            return response;
        }



    }
}
