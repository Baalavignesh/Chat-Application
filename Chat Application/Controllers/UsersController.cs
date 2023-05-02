using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chat_Application;
using Chat_Application.Models;
using Chat_Application.Services;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Chat_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;
        public UsersController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        // GET: api/GetAllUser
        [HttpGet("GetAllUser"), Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var response = await _userInfoService.GetAllUser();
            return Ok(response);
        }

        // GET: api/GetChats/1
        //[HttpGet("GetMyChats/{userId}")]
        //public async Task<ActionResult<IEnumerable<User>>> GetMyChats(int userId)
        //{
        //    Console.WriteLine("controller");
        //    Console.WriteLine(userId);
        //    var response = await _userInfoService.GetMyChats(userId);
        //    Console.WriteLine(response.Error);
        //    if(response.Data != null)
        //    {
        //        return Ok(response.Data);
        //    }
        //    else
        //    {
        //        return NotFound(response.Error);
        //    }
        //}

    }
}
