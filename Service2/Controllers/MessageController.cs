using Microsoft.AspNetCore.Mvc;

namespace Service2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get([FromServices]MessagingQueueService queueService)
        {

            return Ok(queueService.Message.ToArray());
        }
    }
}
