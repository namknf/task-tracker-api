using Microsoft.AspNetCore.Mvc;

namespace TaskTracker.Bot.Controllers
{
    [ApiController]
    [Route("bot/")]
    public class SendingController : ControllerBase
    {
        /// <summary>
        /// Sending message code by phone number
        /// </summary>
        /// <returns></returns>
        [HttpPost("send/code")]
        public IActionResult SendCodeToPhone([FromBody] string code)
        {
            return Ok();
        }
    }
}