namespace TaskTracker.Contract.Repository
{
    public interface IFileRepository
    {
        void CreateFile(Entities.Models.File file);

        Task<Entities.Models.File?> GetFileAsync(Guid fileId, bool trackChanges);
    }
}
