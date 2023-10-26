using Microsoft.EntityFrameworkCore;
using TaskTracker.Contract;
using TaskTracker.Entities.Data;
using TaskTracker.Entities.Models;

namespace TaskTracker.Repository
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(DataContext dataContext) : base(dataContext) { }

        public void CreateProject(Project project) => Create(project);

        public async Task<Project?> GetProjectAsync(Guid projectId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(projectId), trackChanges).SingleOrDefaultAsync();

        public async Task<List<Project>?> GetProjectsAsync(string userId, bool trackChanges) =>
            await FindAll(trackChanges).Include(p => p.Participants)
            .Where(c => c.Participants.Any(p => p.Id.Equals(userId))).ToListAsync();
    }
}
