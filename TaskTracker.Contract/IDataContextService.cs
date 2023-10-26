﻿using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;

namespace TaskTracker.Contract
{
    public interface IDataContextService
    {
        Task<List<Entities.Models.Task>> GetProjectTasksAsync(Guid projectId);

        Task<Project?> GetProjectAsync(Guid projectId);

        Task<List<Project>> GetProjectsAsync(string userId);

        System.Threading.Tasks.Task SaveChangesAsync();

        System.Threading.Tasks.Task CreateProjectAsync(Project project, List<ParticipantDto> participants);

        Task<User?> GetUserInformationAsync(string userId);
    }
}
