using TaskTracker.Entities.Models;
using TaskTracker.Entities.RequestFeatures;
using TaskTracker.Entities.RequestFeatures.Entities;

namespace TaskTracker.Contract.Repository
{
    public interface IProjectRepository
    {
        Task<Project?> GetProjectAsync(Guid projectId, bool trackChanges);

        void CreateProject(Project project);

        void DeleteProject(Project project);

        void UpdateProject(Project project);

        Task<PagedList<Project>?> GetProjectsAsync(string userId, bool trackChanges, ProjectParameters parms);
    }
}
