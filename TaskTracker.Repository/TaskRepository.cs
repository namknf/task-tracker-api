using Microsoft.EntityFrameworkCore;
using TaskTracker.Contract;
using TaskTracker.Entities.Data;
using Task = TaskTracker.Entities.Models.Task;

namespace TaskTracker.Repository
{
    public class TaskRepository : RepositoryBase<Task>, ITaskRepository
    {
        public TaskRepository(DataContext dataContext) : base(dataContext) { }

        public void CreateTask(Task task) => Create(task);

        public void DeleteTask(Task task) => Delete(task);

        public async Task<List<Task>> GetAllTasksForProjectAsync(Guid projectId, bool trackChanges)
        {
            return await FindByCondition(e => e.ProjectId.Equals(projectId), trackChanges).ToListAsync();
        }
    }
}
