using Microsoft.EntityFrameworkCore;
using TaskTracker.Contract;
using TaskTracker.Entities.Data;

namespace TaskTracker.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private DataContext _dataContext;
        private ITaskRepository _taskRepository;
        private IProjectRepository _projectRepository;

        public RepositoryManager(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ITaskRepository TaskRepository
        {
            get
            {
                _taskRepository ??= new TaskRepository(_dataContext);
                return _taskRepository;
            }
        }

        public IProjectRepository ProjectRepository
        {
            get
            {
                _projectRepository ??= new ProjectRepository(_dataContext);
                return _projectRepository;
            }
        }

        public void DetachAllEntities()
        {
            var undetachedEntriesCopy = _dataContext.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Detached)
                .ToList();

            foreach (var entry in undetachedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        public async Task SaveAsync() => await _dataContext.SaveChangesAsync();
    }
}
