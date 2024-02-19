using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.ActionFilters;
using TaskTracker.Entities.DataTransferObjects;

namespace TaskTracker.Api.Controllers
{
    [Route("api/files")]
    [Produces("application/json")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMapper _mapper;

        public FileController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Get file by Id
        /// </summary>
        /// <param name="fileId">file id</param>
        /// <returns></returns>
        [HttpGet("fileId")]
        [ServiceFilter(typeof(ValidateFileExistsAttribute))]
        public ActionResult GetFile(Guid fileId)
        {
            var file = HttpContext.Items["file"] as Entities.Models.File;
            var fileDto = _mapper.Map<FileDto>(file);
            return Ok(fileDto);
        }
    }
}
