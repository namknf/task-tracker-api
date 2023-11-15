using TaskTracker.Entities.Models;

namespace TaskTracker.Contract
{
    public interface IProjectRepository
    {
        Task<Project?> GetProjectAsync(Guid projectId, bool trackChanges);

        void CreateProject(Project project);

        void DeleteProject(Project project);

        void UpdateProject(Project project);

        Task<List<Project>?> GetProjectsAsync(string userId, bool trackChanges);
    }
}
