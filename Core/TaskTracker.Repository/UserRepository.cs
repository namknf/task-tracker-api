using TaskTracker.Contract;
using TaskTracker.Entities.Data;
using TaskTracker.Entities.Models;

namespace TaskTracker.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext) { }
    }
}
