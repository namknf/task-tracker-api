using TaskTracker.Entities.RequestFeatures;
using TaskTracker.Entities.RequestFeatures.Entities;
using Task = TaskTracker.Entities.Models.Task;

namespace TaskTracker.Contract.Repository
{
    public interface ITaskRepository
    {
        Task<PagedList<Task>> GetAllTasksForProjectAsync(Guid projectId, bool trackChanges, TaskParameters parms);

        void CreateTask(Task task);

        void DeleteTask(Task task);

        void UpdateTask(Task task);

        Task<Task?> GetTaskAsync(Guid projectId, Guid taskId, bool trackChanges);
    }
}
