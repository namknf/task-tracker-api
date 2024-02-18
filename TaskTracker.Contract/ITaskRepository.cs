using TaskTracker.Entities.RequestFeatures;
using Task = TaskTracker.Entities.Models.Task;

namespace TaskTracker.Contract
{
    public interface ITaskRepository
    {
        Task<List<Task>> GetAllTasksForProjectAsync(Guid projectId, bool trackChanges, TaskParameters parms);

        void CreateTask(Task task);

        void DeleteTask(Task task);

        void UpdateTask(Task task);

        Task<Task?> GetTaskAsync(Guid projectId, Guid taskId, bool trackChanges);
    }
}
