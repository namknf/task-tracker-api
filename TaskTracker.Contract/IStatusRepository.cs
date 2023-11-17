using TaskTracker.Entities.Models;

namespace TaskTracker.Contract
{
    public interface IStatusRepository
    {
        Task<List<Status>> GetAllStatusesAsync(bool trackChanges);

        Task<Status?> GetStatusAsync(Guid statusId, bool trackChanges);
    }
}
