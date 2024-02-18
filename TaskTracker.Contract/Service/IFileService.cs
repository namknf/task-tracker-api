using Microsoft.AspNetCore.Http;
using TaskTracker.Entities.Models;

namespace TaskTracker.Contract.Service
{
    public interface IFileService
    {
        void UploadPhoto(IFormFile file, User user);
    }
}
