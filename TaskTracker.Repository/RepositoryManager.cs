using Microsoft.EntityFrameworkCore;
using TaskTracker.Contract;
using TaskTracker.Contract.Repository;
using TaskTracker.Entities.Data;

namespace TaskTracker.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private DataContext _dataContext;
        private ITaskRepository _taskRepository;
        private IProjectRepository _projectRepository;
        private IStatusRepository _statusRepository;
        private IPriorityRepository _priorityRepository;
        private IFileRepository _fileRepository;

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

        public IStatusRepository StatusRepository
        {
            get
            {
                _statusRepository ??= new StatusRepository(_dataContext);
                return _statusRepository;
            }
        }

        public IPriorityRepository PriorityRepository
        {
            get
            {
                _priorityRepository ??= new PriorityRepository(_dataContext);
                return _priorityRepository;
            }
        }

        public IFileRepository FileRepository
        {
            get
            {
                _fileRepository ??= new FileRepository(_dataContext);
                return _fileRepository;
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
