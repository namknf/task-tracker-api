using TaskTracker.Entities.Models;

namespace TaskTracker.Contract
{
    public interface IDataContextService
    {
        Task<List<Entities.Models.Task>> GetProjectTasksAsync(Guid projectId);

        Task<Project?> GetProjectAsync(Guid projectId);

        Task<User?> GetUserAsync(Guid id);

        System.Threading.Tasks.Task SaveChangesAsync();
    }
}
