using Microsoft.AspNetCore.Mvc;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.RequestFeatures;

namespace TaskTracker.Api.Controllers
{
    [Route("api/project/{projectId}/")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet("tasks")]  
        public async Task<ActionResult<List<TaskDto>>> GetAllTasksForProject(Guid projectId, [FromQuery] TaskParameters parms)
        {
            return Ok();
        }
    }
}
