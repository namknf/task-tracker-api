using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Contract.Service;
using TaskTracker.Entities.DataTransferObjects;

namespace TaskTracker.Api.Controllers
{
    [Route("api/email")]
    [Produces("application/json")]
    [ApiController]
    [AllowAnonymous]
    public class EmailController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;

        public EmailController(ILogger<EmailController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        /// <summary>
        /// Отправка вопроса на электронную почту
        /// </summary>
        /// <param name="questionDto">Почта и текст вопроса</param>
        /// <response code="404">Некорректные параметры для отправки письма на почту</response>
        /// <response code="200">Вопрос успешно отправлен</response>
        /// <returns></returns>
        [HttpPost("ask")]
        public IActionResult AskQuestion([FromBody] QuestionDto questionDto)
        {
            if (questionDto == null)
            {
                _logger.LogError("Некорректные параметры для отправки письма на почту");
                return BadRequest("Некорректные параметры для отправки письма на почту");
            }
            _emailService.SendQuestionAsync(questionDto.Email, "Заданный вопрос", questionDto.QuestionText);
            return Ok();
        }

        /// <summary>
        /// Отправка ответного сообщения на вопрос
        /// </summary>
        /// <param name="senderDto">Адрес электронной почты для отправки</param>
        /// <response code="404">Некорректные параметры для отправки письма на почту</response>
        /// <response code="200">Ответ успешно отправлен</response>
        /// <returns></returns>
        [HttpPost("answer")]
        public async Task<IActionResult> SendAnswerAsync([FromBody] SenderDto senderDto)
        {
            if (senderDto == null)
            {
                _logger.LogError("Некорректный адрес почты");
                return BadRequest("Некорректный адрес почты");
            }
            await _emailService.SendEmailAsync(senderDto.Email, "Your question", "Добрый день, с вашей стороны был задан вопрос, и в скором времени мы обязательно решим вашу проблему!");
            return Ok();
        }
    }
}
