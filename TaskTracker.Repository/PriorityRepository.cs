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
    }
}
