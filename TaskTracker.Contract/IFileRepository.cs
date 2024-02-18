namespace TaskTracker.Contract
{
    public interface IFileRepository
    {
        void CreateFile(Entities.Models.File file);
    }
}
