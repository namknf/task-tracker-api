using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.ActionFilters;
using TaskTracker.Contract.Service;

namespace TaskTracker.Api.Controllers
{
    [Route("api/files")]
    [Produces("application/json")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IDataContextService _dataContextService;

        public FileController(IDataContextService dataContextService)
        {
            _dataContextService = dataContextService;
        }

        /// <summary>
        /// Get file by Id
        /// </summary>
        /// <param name="fileId">file id</param>
        /// <returns></returns>
        [HttpGet("fileId"), Authorize]
        [ServiceFilter(typeof(ValidateFileExistsAttribute))]
        public ActionResult GetFile(Guid fileId)
        {
            var file = HttpContext.Items["file"] as Entities.Models.File;
            return File(file.Data, "application/octet-stream", file.FileName);
        }

        /// <summary>
        /// Delete existing file
        /// </summary>
        /// <param name="fileId">file id</param>
        /// <returns></returns>
        [HttpDelete("fileId"), Authorize]
        [ServiceFilter(typeof(ValidateFileExistsAttribute))]
        public async Task<ActionResult> DeleteFile(Guid fileId) 
        {
            var file = HttpContext.Items["file"] as Entities.Models.File;
            _dataContextService.DeleteFile(file);
            await _dataContextService.SaveChangesAsync();
            return NoContent();
        }
    }
}
