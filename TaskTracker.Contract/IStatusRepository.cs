using TaskTracker.Entities.Models;

namespace TaskTracker.Contract
{
    public interface IStatusRepository
    {
        Task<List<Status>> GetAllStatusesAsync(bool trackChanges);
    }
}
