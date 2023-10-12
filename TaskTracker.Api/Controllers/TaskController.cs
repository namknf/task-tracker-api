using Microsoft.AspNetCore.Mvc;
using TaskTracker.Entities.Models;
using Task = TaskTracker.Entities.Models.Task;

namespace TaskTracker.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public async Task<ActionResult<List<Task>>> GetAllTasks()
        {
            return Ok();
        }
    }
}
