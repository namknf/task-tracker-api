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
        /// Получение файла по идентификатору
        /// </summary>
        /// <param name="fileId">Идентификатор файла</param>
        /// <response code="206">Файл успешно получен из БД</response>
        /// <response code="200">Файл успешно получен из БД</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns></returns>
        [HttpGet("fileId"), Authorize]
        [ServiceFilter(typeof(ValidateFileExistsAttribute))]
        public ActionResult GetFile(Guid fileId)
        {
            var file = HttpContext.Items["file"] as Entities.Models.File;
            return File(file.Data, "application/octet-stream", file.FileName);
        }

        /// <summary>
        /// Удаление существующего файла
        /// </summary>
        /// <param name="fileId">Идентификатор файла</param>
        /// <response code="204">Файл успешно удален</response>
        /// <response code="200">Файл успешно удален</response>
        /// <response code="404">Файл не был найден</response>
        /// <response code="401">Пользователь не авторизован</response>
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
