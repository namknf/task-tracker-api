using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Contract;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.RequestFeatures;

namespace TaskTracker.Api.Controllers
{
    [Route("api/project/{projectId}/")]
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

        [HttpGet("tasks"), Authorize]  
        public async Task<ActionResult<List<TaskDto>>> GetAllTasksForProject(Guid projectId, [FromQuery] TaskParameters parms)
        {
            var project = await _dataContextService.GetProjectAsync(projectId, false);
            if (project == null)
            {
                _logger.LogInformation("Project with id: {projectId} doesn't exist in the database.", projectId);
                return NotFound();
            }
            var tasksFromDb = await _dataContextService.GetProjectTasksAsync(projectId);
            var tasksDto = _mapper.Map<IEnumerable<TaskDto>>(tasksFromDb);
            return Ok(tasksDto);
        }
    }
}
