using TaskTracker.Entities.Models;
using Task = TaskTracker.Entities.Models.Task;

namespace TaskTracker.Contract
{
    public interface IDataContextService
    {
        Task<List<Task>> GetProjectTasksAsync(Guid projectId);

        Task<Project?> GetProjectAsync(Guid projectId);

        Task<CodeAttempt?> GetCodeAttemptAsync(string phoneNumber, string code);
    }
}
