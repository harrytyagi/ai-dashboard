using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AIDashboard.API.Models;
using AIDashboard.API.Services;

    namespace AIDashboard.API.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
                return BadRequest("Message cannot be empty");

            var reply = await _chatService.GetReplyAsync(request.Message);
            return Ok(new ChatResponse { Reply = reply, Success = true });
        }
    }