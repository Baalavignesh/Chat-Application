using Chat_Application.DTOs;
using Chat_Application.Models;
using Chat_Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Chat_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IUserService _userService;


        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            Console.WriteLine("Register Api called");

            user = await _userService.CheckUser(request);

            if(user == null)
            {
                Console.WriteLine("not found");
            }
            else
            {
                Console.WriteLine(user.Username);
            }

            CreatePasswordHash(request.Password, out byte[] PasswordHash, out byte[] passwordSalt);

            user.PasswordHash = PasswordHash;
            user.PasswordSalt = passwordSalt;
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
