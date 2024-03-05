using Microsoft.EntityFrameworkCore;
using TaskTracker.Contract.Repository;
using TaskTracker.Entities.Data;

namespace TaskTracker.Repository
{
    public class FileRepository : RepositoryBase<Entities.Models.File>, IFileRepository
    {
        public FileRepository(DataContext dataContext) : base(dataContext) { }

        public void CreateFile(Entities.Models.File file) => Create(file);

        public async Task<Entities.Models.File?> GetFileAsync(Guid fileId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(fileId), trackChanges).SingleOrDefaultAsync();

        public void DeleteFile(Entities.Models.File file) => Delete(file);
    }
}
