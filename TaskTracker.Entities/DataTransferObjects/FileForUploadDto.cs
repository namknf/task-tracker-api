using Microsoft.AspNetCore.Http;

namespace TaskTracker.Entities.DataTransferObjects
{
    public class FileForUploadDto
    {
        public IFormFile File { get; set; }
    }
}
