using Microsoft.EntityFrameworkCore;
using TaskTracker.Contract;
using TaskTracker.Entities.Data;
using TaskTracker.Entities.Models;

namespace TaskTracker.Repository
{
    public class StatusRepository : RepositoryBase<Status>, IStatusRepository
    {
        public StatusRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<List<Status>> GetAllStatusesAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();
    }
}
