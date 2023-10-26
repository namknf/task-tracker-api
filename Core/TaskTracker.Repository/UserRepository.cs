using Microsoft.EntityFrameworkCore;
using TaskTracker.Contract;
using TaskTracker.Entities.Data;
using TaskTracker.Entities.Models;

namespace TaskTracker.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<User?> GetUserAsync(Guid projectId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(projectId), trackChanges)
                    .Include(u => u.Projects)
                    .Include(u => u.Tasks)
                    .SingleOrDefaultAsync();
    }
}
