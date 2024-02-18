using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskTracker.Api.Controllers
{
    [Route("api/files")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class FIleController : ControllerBase
    {
        [HttpPost("profilePhoto")]
        public IActionResult UploadProfilePhoto()
        {
            return Ok();
        }
    }
}
