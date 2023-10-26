using TaskTracker.Contract;
using TaskTracker.Entities.Models;

namespace TaskTracker.Service
{
    public class DataContextService : IDataContextService
    {
        private readonly IRepositoryManager _manager;

        public DataContextService(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<List<Entities.Models.Task>> GetProjectTasksAsync(Guid projectId) =>
            await _manager.TaskRepository.GetAllTasksForProjectAsync(projectId, false);

        public async Task<Project?> GetProjectAsync(Guid projectId) =>
            await _manager.ProjectRepository.GetProjectAsync(projectId, false);

        public async System.Threading.Tasks.Task SaveChangesAsync() =>
            await _manager.SaveAsync();

        public async Task<List<Project>> GetProjectsAsync() =>
            await _manager.ProjectRepository.GetProjectsAsync(false) ?? new List<Project>();
    }
}
