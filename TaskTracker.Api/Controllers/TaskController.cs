using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskTracker.Api.ActionFilters;
using TaskTracker.Contract;
using TaskTracker.Entities.DataTransferObjects;

namespace TaskTracker.Api.Controllers
{
    [Route("api/projects/{projectId}/tasks/")]
    [Produces("application/json")]
    [ApiController]
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
        /// Get all tasks from project
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <returns>List of tasks</returns>
        /// <response code="200">Successfully get all tasks</response>
        /// <response code="404">Project not found</response>
        [HttpGet, Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ServiceFilter(typeof(ValidateProjectExistsAttribute))]
        public async Task<ActionResult<List<TaskDto>>> GetAllTasksForProject(Guid projectId)
        {
            var tasksFromDb = await _dataContextService.GetProjectTasksAsync(projectId);
            var tasksDto = _mapper.Map<IEnumerable<TaskDto>>(tasksFromDb);
            return Ok(tasksDto);
        }

        /// <summary>
        /// Get task from project by id
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="taskId">task id</param>
        /// <response code="404">Project or task not found</response>
        /// <response code="200">Task was successfully got</response>
        /// <returns>Task</returns>
        [HttpGet("{taskId}"), Authorize]
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
        /// Create new task
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <param name="taskDto">new task model</param>
        /// <response code="404">Project not found</response>
        /// <response code="400">Invalid Task dto</response>
        /// <response code="201">Task was successfully created</response>
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
            return CreatedAtRoute("CreateTaskForProject", new { id = taskToReturn.Id }, taskToReturn);
        }

        /// <summary>
        /// Delete existing task
        /// </summary>
        /// <response code="404">Project or task not found</response>
        /// <response code="204">Task was successfully deleted</response>
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
        /// Update task information
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <param name="taskId">Task id</param>
        /// <param name="taskDto">Updated task model</param>
        /// <response code="404">Project or task not found</response>
        /// <response code="204">Task was successfully updated</response>
        /// <returns>Updated task model</returns>
        [HttpPut("{taskId}"), Authorize]
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
