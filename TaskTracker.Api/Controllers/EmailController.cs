using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Contract.Service;
using TaskTracker.Entities.DataTransferObjects;

namespace TaskTracker.Api.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;

        public EmailController(ILogger<AccountController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        /// <summary>
        /// Send a question on email
        /// </summary>
        /// <param name="questionDto">Email and question text</param>
        /// <returns></returns>
        [HttpPost("ask"), AllowAnonymous]
        public IActionResult AskQuestion([FromBody] QuestionDto questionDto)
        {
            if (questionDto == null)
            {
                _logger.LogError("Invalid parameters to send the e-mail");
                return BadRequest("Invalid parameters to send the e-mail");
            }
            _emailService.SendQuestionAsync(questionDto.Email, "Ask question", questionDto.QuestionText);
            return Ok();
        }

        /// <summary>
        /// Send an answer
        /// </summary>
        /// <param name="senderDto">Email to send</param>
        /// <returns></returns>
        [HttpPost("answer"), AllowAnonymous]
        public async Task<IActionResult> SendAnswerAsync([FromBody] SenderDto senderDto)
        {
            if (senderDto == null)
            {
                _logger.LogError("Invalid e-mail");
                return BadRequest("Invalid e-mail");
            }
            await _emailService.SendEmailAsync(senderDto.Email, "Your question", "Hello, we have seen your question and will try to answer it shortly!");
            return Ok();
        }
    }
}
