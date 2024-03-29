﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.ActionFilters;

namespace TaskTracker.Api.Controllers
{
    [Route("api/files")]
    [Produces("application/json")]
    [ApiController]
    public class FileController : ControllerBase
    {
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
    }
}
