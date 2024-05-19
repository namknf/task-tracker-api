using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using TaskTracker.Api.ActionFilters;
using TaskTracker.Contract.Service;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;
using TaskTracker.Entities.RequestFeatures;

namespace TaskTracker.Api.Controllers
{
    [Route("api/projects/{projectId}/tasks/{taskId}/comments")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class CommentController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IDataContextService _dataContextService;

        public CommentController(ILogger<CommentController> logger, IMapper mapper, IDataContextService dataContextService)
        {
            _logger = logger;
            _mapper = mapper;
            _dataContextService = dataContextService;
        }

        /// <summary>
        /// Получение всех комментариев задачи
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <param name="parms">Параметры для загрузки данных</param>
        /// <response code="200">Комментарии успешно загружены из БД</response>
        /// <response code="404">Не найден проект или задача</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ServiceFilter(typeof(ValidateTaskExistsAttribute))]
        public async Task<ActionResult<List<CommentDto>>> GetAllCommentsForTask(Guid projectId, Guid taskId, [FromQuery] CommentParameters parms)
        {
            var commentsFromDb = await _dataContextService.GetTaskCommentsAsync(taskId, parms);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(commentsFromDb.MetaData));
            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(commentsFromDb);
            return Ok(commentsDto);
        }

        /// <summary>
        /// Получение комментария
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <param name="commentId">Идентификатор комментария</param>
        /// <response code="200">Комментарий успешно загружен из БД</response>
        /// <response code="404">Не найден проект или задача</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns></returns>
        [HttpGet("{commentId}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ServiceFilter(typeof(ValidateCommentExistsAttribute))]
        public ActionResult<CommentDto> GetComment(Guid projectId, Guid taskId, Guid commentId)
        {
            var comment = HttpContext.Items["comment"] as TaskComment;
            var commentDto = _mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }

        /// <summary>
        /// Создание нового комментария
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <param name="commentDto">Модель комментария для создания нового</param>
        /// <response code="201">Комментарий успешно создан</response>
        /// <response code="404">Не найден проект или задача</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns></returns>
        [HttpPost(Name = "CreateCommentForTask")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ServiceFilter(typeof(ValidateTaskExistsAttribute))]
        public async Task<IActionResult> CreateCommentForTask(Guid projectId, Guid taskId, [FromBody] CommentForCreationDto commentDto)
        {
            if (commentDto == null)
            {
                _logger.LogError("Отсутствует информация для создания комментария");
                return BadRequest("Отсутствует информация для создания комментария");
            }

            var commentEntity = _mapper.Map<TaskComment>(commentDto);
            _dataContextService.CreateComment(commentEntity, UserId, taskId);
            await _dataContextService.SaveChangesAsync();
            var commentToReturn = _mapper.Map<CommentDto>(commentEntity);
            return CreatedAtRoute("CreateCommentForTask", new { id = commentToReturn.Id }, commentToReturn);
        }

        /// <summary>
        /// Удаление комментария
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <param name="commentId">Идентификатор комментария</param>
        /// <response code="204">Комментарий успешно удален</response>
        /// <response code="404">Не найден проект или задача</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns></returns>
        [HttpDelete("{commentId}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ServiceFilter(typeof(ValidateCommentExistsAttribute))]
        public async Task<IActionResult> DeleteComment(Guid projectId, Guid taskId, Guid commentId)
        {
            var comment = HttpContext.Items["comment"] as TaskComment;
            _dataContextService.DeleteComment(comment);
            await _dataContextService.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Редактирование комментария
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <param name="commentId">Идентификатор комментария</param>
        /// <response code="204">Комментарий успешно обновлен</response>
        /// <response code="404">Не найден проект или задача; отсутствует модель для обновления информации</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="422">Обновить данные о комментарии не получилось, так как данные были неккорректны</response>
        /// <returns></returns>
        [HttpPatch("{commentId}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ServiceFilter(typeof(ValidateCommentExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateComment(Guid projectId, Guid taskId, Guid commentId, [FromBody] JsonPatchDocument<CommentForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("Отсутствует модель для обновления информации комментария");
                return BadRequest("Отсутствует модель для обновления информации комментария");
            }

            var commentEntity = HttpContext.Items["comment"] as TaskComment;
            var commentToPatch = _mapper.Map<CommentForUpdateDto>(commentEntity);
            patchDoc.ApplyTo(commentToPatch, ModelState);
            TryValidateModel(commentToPatch);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Некорректное состояние модели для обновления");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(commentToPatch, commentEntity);
            await _dataContextService.SaveChangesAsync();
            return NoContent();
        }
    }
}
