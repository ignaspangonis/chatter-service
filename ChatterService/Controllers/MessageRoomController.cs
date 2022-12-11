using ChatterService.Hubs;
using ChatterService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatterService.Controllers
{
    [ApiController]
    [Route("message-room")]
    public class MessageRoomController : ControllerBase
    {
        private readonly ILogger<WeatherController> logger;
        private readonly MessageService messageService;

        public MessageRoomController(ILogger<WeatherController> logger, MessageService messageService)
        {
            this.logger = logger;
            this.messageService = messageService;
        }

        [HttpDelete("{roomName}")]
        public async Task<IActionResult> Delete(string roomName)
        {
            try
            {
                await messageService.DeleteRoomAsync(roomName);
            }
            catch (Exception exception)
            {
                logger.Log(LogLevel.Error, "Error", exception);
            }


            return Ok(new { message = "Room deleted" });
        }
    }
}

