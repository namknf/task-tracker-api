using Task = TaskTracker.Entities.Models.Task;

namespace TaskTracker.Contract.Repository
{
    public interface ITaskRepository
    {
        Task<List<Task>> GetAllTasksForProjectAsync(Guid projectId, bool trackChanges);

        void CreateTask(Task task);

        void DeleteTask(Task task);

        void UpdateTask(Task task);

        Task<Task?> GetTaskAsync(Guid projectId, Guid taskId, bool trackChanges);
    }
}
