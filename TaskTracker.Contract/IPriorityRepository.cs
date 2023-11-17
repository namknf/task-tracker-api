using TaskTracker.Entities.Models;

namespace TaskTracker.Contract
{
    public interface IPriorityRepository
    {
        Task<List<TaskPriority>> GetAllPrioritiesAsync(bool trackChanges);

        Task<TaskPriority?> GetPriorityAsync(Guid priorityId, bool trackChanges);
    }
}
