﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Contract.Repository;
using TaskTracker.Contract.Service;
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

        public void UpdateProject(Project project) =>
            _manager.ProjectRepository.UpdateProject(project);
            

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
    }
}
