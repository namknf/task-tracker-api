using Microsoft.AspNetCore.Mvc;
using TaskTracker.Entities.DataTransferObjects;
using Task = TaskTracker.Entities.Models.Task;

namespace TaskTracker.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet("tasks")]
        public async Task<ActionResult<List<TaskDto>>> GetAllTasks()
        {
            return Ok();
        }
    }
}
