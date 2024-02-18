using Microsoft.AspNetCore.Http;
using TaskTracker.Entities.Models;

namespace TaskTracker.Contract
{
    public interface IFileService
    {
        void UploadPhoto(IFormFile file, User user);
    }
}
