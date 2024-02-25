using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Contract.Repository;
using TaskTracker.Contract.Service;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;
using TaskTracker.Entities.RequestFeatures;
using TaskTracker.Entities.RequestFeatures.Entities;

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

        public async Task<PagedList<Entities.Models.Task>> GetProjectTasksAsync(Guid projectId, TaskParameters parms) =>
            await _manager.TaskRepository.GetAllTasksForProjectAsync(projectId, false, parms);

        public async Task<Project?> GetProjectAsync(Guid projectId, bool trackChanges) =>
            await _manager.ProjectRepository.GetProjectAsync(projectId, trackChanges);

        public void UpdateProject(Project project) =>
            _manager.ProjectRepository.UpdateProject(project);
            

        public async System.Threading.Tasks.Task SaveChangesAsync() =>
            await _manager.SaveAsync();

        public async Task<PagedList<Project>> GetProjectsAsync(string userId, ProjectParameters parms) =>
            await _manager.ProjectRepository.GetProjectsAsync(userId, false, parms);

        public async System.Threading.Tasks.Task CreateProjectAsync(Project project, List<ParticipantDto> participants)
        {
            var users = new List<User>();
            foreach (var part in participants)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.Equals(part.Id.ToString()));
                if (user == null) throw new Exception($"User with id {part.Id} does not exist");
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
                if (user == null) throw new Exception($"User with id {part.Id} does not exist");
                else users.Add(user);
            }
            taskEntity.Participants = users;
            taskEntity.ProjectId = projectId;
            _manager.TaskRepository.CreateTask(taskEntity);
        }

        public void DeleteTask(Entities.Models.Task task) => _manager.TaskRepository.DeleteTask(task);

        public async Task<Entities.Models.Task?> GetTaskAsync(Guid projectId, Guid taskId, bool trackChanges) =>
            await _manager.TaskRepository.GetTaskAsync(projectId, taskId, trackChanges);

        public void UpdateTask(Entities.Models.Task task) =>
            _manager.TaskRepository.UpdateTask(task);

        public async Task<List<Status>> GetAllStatuses() =>
            await _manager.StatusRepository.GetAllStatusesAsync(false);

        public async Task<List<TaskPriority>> GetAllPriorities() =>
            await _manager.PriorityRepository.GetAllPrioritiesAsync(false);

        public async Task<TaskPriority?> GetPriorityAsync(Guid priorityId, bool trackChanges) =>
            await _manager.PriorityRepository.GetPriorityAsync(priorityId, trackChanges);

        public async Task<Status?> GetStatusAsync(Guid statusId, bool trackChanges) =>
            await _manager.StatusRepository.GetStatusAsync(statusId, trackChanges);

        public async Task<PagedList<User>> GetParticipantsAsync(ParticipantParameters parms)
        {
            var users = await _userManager.Users.AsNoTracking().ToListAsync();
            return PagedList<User>.ToPagedList(users, parms.PageNumber, parms.PageSize);
        }

        public async Task<Entities.Models.File?> GetFileAsync(Guid fileId, bool trackChanges) =>
            await _manager.FileRepository.GetFileAsync(fileId, trackChanges);

        public async Task<PagedList<TaskComment>> GetTaskCommentsAsync(Guid taskId, CommentParameters parms) =>
            await _manager.CommentRepository.GetAllCommentsForTaskAsync(taskId, false, parms);

        public void CreateComment(TaskComment commentEntity, string userId, Guid taskId)
        {
            commentEntity.UserId = userId;
            commentEntity.TaskId = taskId;
            _manager.CommentRepository.CreateComment(commentEntity);
        }

        public async Task<TaskComment?> GetCommentAsync(Guid taskId, Guid commentId, bool trackChanges) =>
            await _manager.CommentRepository.GetCommentAsync(taskId, commentId, trackChanges);

        public void DeleteComment(TaskComment comment) =>
            _manager.CommentRepository.DeleteComment(comment);

        public void UpdateComment(TaskComment comment) =>
            _manager.CommentRepository.UpdateComment(comment);
    }
}
