using Chat_Application.DTOs;
using Chat_Application.Models;
using Chat_Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SingleChatController : ControllerBase
    {
        private readonly ISingleChatService _singleChatService;


        public SingleChatController(ISingleChatService singleChatService) { 
            _singleChatService = singleChatService;
        }


        [HttpGet("GetSingleChat/{chatId}"), Authorize]
        public async Task<ActionResult<SingleChat>> GetSingleChat(int chatId)
        {
            var response = await _singleChatService.GetSingleChat(chatId);

            return Ok(response);
        }


        [HttpPost("AddSingleChatMessage"), Authorize]
        public async Task<ActionResult<bool>> AddSingleChatMessage(NewSingleChatDto request)
        {
            await _singleChatService.AddSingleChat(request);

            return Ok(true);
        }


    }
}
