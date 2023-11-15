using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Contract;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;

namespace TaskTracker.Service
{
    public class DataContextService : IDataContextService
    {
        private readonly IRepositoryManager _manager;
        private readonly UserManager<User> _userManager;

        public DataContextService(IRepositoryManager manager, UserManager<User> userManager)
        {
            _manager = manager;
            _userManager = userManager;
        }

        public async Task<List<Entities.Models.Task>> GetProjectTasksAsync(Guid projectId) =>
            await _manager.TaskRepository.GetAllTasksForProjectAsync(projectId, false);

        public async Task<Project?> GetProjectAsync(Guid projectId, bool trackChanges) =>
            await _manager.ProjectRepository.GetProjectAsync(projectId, trackChanges);

        public async System.Threading.Tasks.Task SaveChangesAsync() =>
            await _manager.SaveAsync();

        public async Task<List<Project>> GetProjectsAsync(string userId) =>
            await _manager.ProjectRepository.GetProjectsAsync(userId, false) ?? new List<Project>();

        public async System.Threading.Tasks.Task CreateProjectAsync(Project project, List<ParticipantDto> participants)
        {
            var users = new List<User>();
            foreach (var part in participants)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.Equals(part.Id.ToString()));
                if (user == null) continue;
                else users.Add(user);
            }
            project.Participants = users;
            _manager.ProjectRepository.CreateProject(project);
        }

        public async Task<User?> GetUserInformationAsync(string userId)
        {
            return await _userManager.Users
                .Include(u => u.Projects)
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Id.Equals(userId));
        }

        public void DeleteProject(Project project) =>
            _manager.ProjectRepository.DeleteProject(project);

        public async System.Threading.Tasks.Task CreateTaskAsync(Entities.Models.Task taskEntity, List<ParticipantDto> participants, Guid projectId)
        {
            var users = new List<User>();
            foreach (var part in participants)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.Equals(part.Id.ToString()));
                if (user == null) continue;
                else users.Add(user);
            }
            taskEntity.Participants = users;
            taskEntity.ProjectId = projectId;
            _manager.TaskRepository.CreateTask(taskEntity);
        }

        public void DeleteTask(Entities.Models.Task task) => _manager.TaskRepository.DeleteTask(task);

        public async Task<Entities.Models.Task?> GetTaskAsync(Guid projectId, Guid taskId, bool trackChanges) =>
            await _manager.TaskRepository.GetTaskAsync(projectId, taskId, trackChanges);
    }
}
