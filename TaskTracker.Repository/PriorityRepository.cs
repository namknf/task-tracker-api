using Microsoft.EntityFrameworkCore;
using TaskTracker.Contract;
using TaskTracker.Entities.Data;
using TaskTracker.Entities.Models;

namespace TaskTracker.Repository
{
    public class PriorityRepository : RepositoryBase<TaskPriority>, IPriorityRepository
    {
        public PriorityRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<List<TaskPriority>> GetAllPrioritiesAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<TaskPriority?> GetPriorityAsync(Guid priorityId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(priorityId), trackChanges).SingleOrDefaultAsync();
    }
}
