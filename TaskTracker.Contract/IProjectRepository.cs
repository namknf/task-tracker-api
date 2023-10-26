using TaskTracker.Entities.Models;

namespace TaskTracker.Contract
{
    public interface IProjectRepository
    {
        Task<Project?> GetProjectAsync(Guid projectId, bool trackChanges);

        void CreateProject(Project project);

        Task<List<Project>?> GetProjectsAsync(bool trackChanges);
    }
}
