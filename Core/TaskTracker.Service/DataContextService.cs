using TaskTracker.Contract;
using TaskTracker.Entities.Models;
using Task = TaskTracker.Entities.Models.Task;

namespace TaskTracker.Service
{
    public class DataContextService : IDataContextService
    {
        private readonly IRepositoryManager _manager;

        public DataContextService(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<List<Task>> GetProjectTasksAsync(Guid projectId, bool trackChanges) =>
            await _manager.TaskRepository.GetAllTasksForProjectAsync(projectId, trackChanges);

        public async Task<Project?> GetProjectAsync(Guid projectId, bool trackChanges) =>
            await _manager.ProjectRepository.GetProjectAsync(projectId, trackChanges);
    }
}
