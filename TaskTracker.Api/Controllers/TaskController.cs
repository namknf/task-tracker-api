using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using TaskTracker.Api.ActionFilters;
using TaskTracker.Contract.Service;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.RequestFeatures;

namespace TaskTracker.Api.Controllers
{
    [Route("api/projects/{projectId}/tasks/")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDataContextService _dataContextService;
        private readonly ILogger _logger;

        public TaskController(IMapper mapper, IDataContextService dataContextService, ILogger<TaskController> logger)
        {
            _mapper = mapper;
            _dataContextService = dataContextService;
            _logger = logger;
        }

        /// <summary>
        /// Получение списка всех задач проекта
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <param name="parms">Параметры для пагинации</param>
        /// <returns>List of tasks</returns>
        /// <response code="200">Список задач успешно загружен из БД</response>
        /// <response code="404">Проект не найден</response>
        /// <response code="401">Пользователь не авторизован</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ServiceFilter(typeof(ValidateProjectExistsAttribute))]
        public async Task<ActionResult<List<TaskDto>>> GetAllTasksForProject(Guid projectId, [FromQuery] TaskParameters parms)
        {
            var tasksFromDb = await _dataContextService.GetProjectTasksAsync(projectId, parms);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(tasksFromDb.MetaData));
            var tasksDto = _mapper.Map<IEnumerable<TaskDto>>(tasksFromDb);
            return Ok(tasksDto);
        }

        /// <summary>
        /// Получение задачи
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <response code="404">Проект или задача не найдена</response>
        /// <response code="200">Информация о задаче успешно загружена из БД</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns>Task</returns>
        [HttpGet("{taskId}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ServiceFilter(typeof(ValidateTaskExistsAttribute))]
        public ActionResult<TaskDto> GetTask(Guid projectId, Guid taskId)
        {
            var task = HttpContext.Items["task"] as Entities.Models.Task;
            var taskDto =_mapper.Map<TaskDto>(task);
            return Ok(taskDto);
        }

        /// <summary>
        /// Создание новой задачи
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <param name="taskDto">Параметры для создания</param>
        /// <response code="404">Проект не был найден</response>
        /// <response code="400">Некорректные параметры для создания задачи</response>
        /// <response code="201">Задача успешно создана</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns>Created task</returns>
        [HttpPost(Name = "CreateTaskForProject")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ServiceFilter(typeof(ValidateProjectExistsAttribute))]
        public async Task<IActionResult> CreateTaskForProject(Guid projectId, [FromBody] TaskForCreationDto taskDto)
        {
            if (taskDto == null)
            {
                _logger.LogError("TaskForCreationDto is null");
                return BadRequest("TaskForCreationDto is null");
            }

            var taskEntity = _mapper.Map<Entities.Models.Task>(taskDto);
            await _dataContextService.CreateTaskAsync(taskEntity, taskDto.Participants, projectId);
            await _dataContextService.SaveChangesAsync();
            var taskToReturn = _mapper.Map<TaskDto>(taskEntity);
            taskToReturn.Status = _mapper.Map<StatusDto>(await _dataContextService.GetStatusAsync(taskEntity.TaskStatusId, false));
            taskToReturn.Priority = _mapper.Map<PriorityDto>(await _dataContextService.GetPriorityAsync(taskEntity.TaskPriorityId, false));
            return CreatedAtRoute("CreateTaskForProject", new { id = taskToReturn.Id }, taskToReturn);
        }

        /// <summary>
        /// Удаление существующей задачи
        /// </summary>
        /// <response code="404">Проект или задача не найдены</response>
        /// <response code="204">Задача успешно удалена</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns>No Content</returns>
        [HttpDelete("{taskId}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ServiceFilter(typeof(ValidateTaskExistsAttribute))]
        public async Task<IActionResult> DeleteTask(Guid projectId, Guid taskId)
        {
            var task = HttpContext.Items["task"] as Entities.Models.Task;
            _dataContextService.DeleteTask(task);
            await _dataContextService.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Обновление информации о задаче
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <param name="taskDto">Параметры для редактирования</param>
        /// <response code="404">Проект или задача не найдены</response>
        /// <response code="204">Информация о задаче успешно обновлена</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns>Updated task model</returns>
        [HttpPut("{taskId}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ServiceFilter(typeof(ValidateTaskExistsAttribute))]
        public async Task<IActionResult> UpdateTask(Guid projectId, Guid taskId, [FromBody] TaskForUpdateDto taskDto)
        {
            var taskEntity = HttpContext.Items["task"] as Entities.Models.Task;
            var updatedTask = _mapper.Map<TaskForUpdateDto, Entities.Models.Task>(taskDto, taskEntity);
            _dataContextService.UpdateTask(updatedTask);
            await _dataContextService.SaveChangesAsync();
            return NoContent();
        }
    }
}
