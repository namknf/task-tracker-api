using TaskTracker.Entities.Models;

namespace TaskTracker.Contract.Repository
{
    public interface IStatusRepository
    {
        Task<List<Status>> GetAllStatusesAsync(bool trackChanges);

        Task<Status?> GetStatusAsync(Guid statusId, bool trackChanges);
    }
}
