using TaskTracker.Entities.Models;

namespace TaskTracker.Contract
{
    public interface IDataContextService
    {
        Task<List<Entities.Models.Task>> GetProjectTasksAsync(Guid projectId);

        Task<Project?> GetProjectAsync(Guid projectId);

        System.Threading.Tasks.Task SaveChangesAsync();
    }
}
