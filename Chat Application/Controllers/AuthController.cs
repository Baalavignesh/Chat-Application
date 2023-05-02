using Azure;
using Chat_Application.DTOs;
using Chat_Application.Models;
using Chat_Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Chat_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;

        public static User db_response = new User();

        public AuthController(IUserService userService, IPasswordService passwordService, IJwtService jwtService)
        {
            _userService = userService;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var db_response = await _userService.CheckUser(request);

            if(db_response.Data == null)
            {
                _passwordService.CreatePasswordHash(request.Password, out byte[] PasswordHash, out byte[] passwordSalt);
                var response = await _userService.RegisterUser(request.Username, PasswordHash, passwordSalt);
                return Ok(response.Data);
            }
            else
            {
                return NotFound(new { db_response.Error });
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var response = await _userService.LoginUser(request);
            if(response.Data == null)
            {
                return NotFound(new { response.Error });
            }

            var passwordResponse = _passwordService.VerifyPasswordHash(request.Password, response.Data.PasswordHash, response.Data.PasswordSalt);
            if (!passwordResponse.Data)
            {
                return NotFound(new { passwordResponse.Error });
            }

            string jwt = _jwtService.CreateToken(response.Data);
            return Ok(new {jwt, response.Data});
        }



    }
}
