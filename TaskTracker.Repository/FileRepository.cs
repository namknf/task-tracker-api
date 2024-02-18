using TaskTracker.Contract;
using TaskTracker.Entities.Data;

namespace TaskTracker.Repository
{
    public class FileRepository : RepositoryBase<Entities.Models.File>, IFileRepository
    {
        public FileRepository(DataContext dataContext) : base(dataContext) { }

        public void CreateFile(Entities.Models.File file)
        {
            Create(file);
        }
    }
}
