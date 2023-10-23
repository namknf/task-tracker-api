using TaskTracker.Entities.Models;
using Task = TaskTracker.Entities.Models.Task;

namespace TaskTracker.Contract
{
    public interface IDataContextService
    {
        Task<List<Task>> GetProjectTasksAsync(Guid projectId, bool trackChanges);

        Task<Project?> GetProjectAsync(Guid projectId, bool trackChanges);
    }
}
