using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet, Authorize]
        [ServiceFilter(typeof(ValidateProjectExistsAttribute))]
        public async Task<ActionResult<List<TaskDto>>> GetAllTasksForProject(Guid projectId)
        {
            var tasksFromDb = await _dataContextService.GetProjectTasksAsync(projectId);
            var tasksDto = _mapper.Map<IEnumerable<TaskDto>>(tasksFromDb);
            return Ok(tasksDto);
        }

        /// <summary>
        /// Create new task
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <param name="taskDto">new task model</param>
        /// <returns>Created task</returns>
        [HttpPost(Name = "CreateTaskForProject")]
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
        /// <returns>No Content</returns>
        [HttpDelete("{taskId}")]
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
        /// <returns>Updated task model</returns>
        [HttpPut("{taskId}"), Authorize]
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
