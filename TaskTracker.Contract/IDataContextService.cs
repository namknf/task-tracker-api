using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;
using TaskTracker.Entities.RequestFeatures;

namespace TaskTracker.Contract
{
    public interface IDataContextService
    {
        Task<List<Entities.Models.Task>> GetProjectTasksAsync(Guid projectId, TaskParameters parms);

        Task<Project?> GetProjectAsync(Guid projectId, bool trackChanges);

        Task<List<Project>> GetProjectsAsync(string userId);

        System.Threading.Tasks.Task SaveChangesAsync();

        System.Threading.Tasks.Task CreateProjectAsync(Project project, List<ParticipantDto> participants);

        Task<User?> GetUserInformationAsync(string userId);

        void DeleteProject(Project project);

        System.Threading.Tasks.Task CreateTaskAsync(Entities.Models.Task taskEntity, List<ParticipantDto> participants, Guid projectId);

        void DeleteTask(Entities.Models.Task task);

        Task<Entities.Models.Task?> GetTaskAsync(Guid projectId, Guid taskId, bool trackChanges);

        void UpdateProject(Project project);

        void UpdateTask(Entities.Models.Task task);

        Task<List<Status>> GetAllStatuses();

        Task<List<TaskPriority>> GetAllPriorities();

        Task<Status?> GetStatusAsync(Guid statusId, bool trackChanges);

        Task<TaskPriority?> GetPriorityAsync(Guid priorityId, bool trackChanges);
    }
}
