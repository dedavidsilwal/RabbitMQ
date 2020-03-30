using Microsoft.AspNetCore.Mvc;

namespace Service1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string message, [FromServices]MessagingQueueService queueService)
        {
            queueService.Enqueue(message);
            return Ok(message);
        }
    }
}
