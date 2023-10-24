using Microsoft.AspNetCore.Mvc;
using TaskTracker.Bot.DataTransferObjects;
using TLSharp.Core;

namespace TaskTracker.Bot.Controllers
{
    [ApiController]
    [Route("bot/")]
    public class SendingController : ControllerBase
    {
        private static IConfiguration _configuration;

        public SendingController() 
        {
        }

        /// <summary>
        /// Sending message code by phone number
        /// </summary>
        /// <returns></returns>
        [HttpPost("send/code")]
        public async Task<IActionResult> SendCodeToPhone([FromBody] CodeInfoDto code)
        {
            return Ok();
        }
    }
}