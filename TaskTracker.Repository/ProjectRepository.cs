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

        public void DeleteProject(Project project) => Delete(project);

        public void UpdateProject(Project project) => Update(project);

        public async Task<Project?> GetProjectAsync(Guid projectId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(projectId), trackChanges)
            .Include(p => p.Participants)
            .Include(p => p.Tasks)
            .SingleOrDefaultAsync();

        public async Task<List<Project>?> GetProjectsAsync(string userId, bool trackChanges) =>
            await FindAll(trackChanges).Include(p => p.Participants)
            .Include(p => p.Tasks)
            .Where(c => c.Participants.Any(p => p.Id.Equals(userId)))
            .ToListAsync();
    }
}
