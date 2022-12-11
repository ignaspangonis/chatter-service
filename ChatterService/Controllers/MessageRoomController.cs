using ChatterService.Hubs;
using ChatterService.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatterService.Controllers
{
    [ApiController]
    [Route("message-room")]
    public class MessageRoomController : ControllerBase
    {
        private readonly ILogger<WeatherController> logger;
        private readonly MessageService messageService;
        //private readonly ChatHub chatHub;

        public MessageRoomController(ILogger<WeatherController> logger, MessageService messageService) // , ChatHub chatHub
        {
            this.logger = logger;
            this.messageService = messageService;
            //this.chatHub = chatHub;
        }

        [HttpDelete("{roomName}")]
        public async Task<IActionResult> Delete(string roomName)
        {
            await messageService.DeleteRoomAsync(roomName);
            //await chatHub.DeleteMessageRoom();

            return Ok(new { message = "Room deleted" });
        }
    }
}

