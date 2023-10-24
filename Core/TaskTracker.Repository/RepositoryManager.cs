using TaskTracker.Contract;
using TaskTracker.Entities.Data;

namespace TaskTracker.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private DataContext _dataContext;
        private ITaskRepository _taskRepository;
        private IProjectRepository _projectRepository;
        private ICodeAttemptRepository _codeAttemptRepository;

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

        public ICodeAttemptRepository CodeAttemptRepository
        {
            get
            {
                _codeAttemptRepository ??= new CodeAttemptRepository(_dataContext);
                return _codeAttemptRepository;
            }
        }

        public Task SaveAsync() => _dataContext.SaveChangesAsync();
    }
}
