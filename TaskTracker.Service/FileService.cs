using Microsoft.AspNetCore.Http;
using TaskTracker.Contract;
using TaskTracker.Entities.Models;

namespace TaskTracker.Service
{
    public class FileService : IFileService
    {
        private readonly IRepositoryManager _manager;

        public FileService(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public void UploadPhoto(IFormFile file, User user)
        {
            byte[]? data = null;

            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                data = binaryReader.ReadBytes((int)file.Length);
            }

            var image = new Entities.Models.File()
            {
                FileName = file.FileName,
                Data = data,
                Title = file.Name
            };
            user.Photo = image;
            _manager.FileRepository.CreateFile(image);
        }
    }
}
