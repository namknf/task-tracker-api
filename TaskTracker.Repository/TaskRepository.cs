﻿using Microsoft.EntityFrameworkCore;
using TaskTracker.Contract.Repository;
using TaskTracker.Entities.Data;
using Task = TaskTracker.Entities.Models.Task;

namespace TaskTracker.Repository
{
    public class TaskRepository : RepositoryBase<Task>, ITaskRepository
    {
        public TaskRepository(DataContext dataContext) : base(dataContext) { }

        public void CreateTask(Task task) => Create(task);

        public void DeleteTask(Task task) => Delete(task);

        public void UpdateTask(Task task) => Update(task);

        public async Task<List<Task>> GetAllTasksForProjectAsync(Guid projectId, bool trackChanges)
        {
            return await FindByCondition(e => e.ProjectId.Equals(projectId), trackChanges)
                .Include(t => t.Participants)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .ToListAsync();
        }

        public async Task<Task?> GetTaskAsync(Guid projectId, Guid taskId, bool trackChanges) =>
            await FindByCondition(e => e.ProjectId.Equals(projectId) && e.Id.Equals(taskId), trackChanges)
            .Include(t => t.Priority)
            .Include(t => t.Status)
            .Include(t => t.Participants)
            .SingleOrDefaultAsync();
    }
}
