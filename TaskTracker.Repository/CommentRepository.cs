using TaskTracker.Contract.Repository;
using TaskTracker.Entities.Data;
using TaskTracker.Entities.Models;
using TaskTracker.Entities.RequestFeatures.Entities;
using TaskTracker.Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace TaskTracker.Repository
{
    public class CommentRepository : RepositoryBase<TaskComment>, ICommentRepository
    {
        public CommentRepository(DataContext dataContext) : base(dataContext) { }

        public void CreateComment(TaskComment comment) => Create(comment);

        public void DeleteComment(TaskComment comment) => Delete(comment);

        public void UpdateComment(TaskComment comment) => Update(comment);

        public async Task<PagedList<TaskComment>> GetAllCommentsForTaskAsync(Guid taskId, bool trackChanges, CommentParameters parms)
        {
            var comments = await FindByCondition(e => e.TaskId.Equals(taskId), trackChanges).ToListAsync();
            return PagedList<TaskComment>.ToPagedList(comments, parms.PageNumber, parms.PageSize);
        }

        public async Task<TaskComment?> GetCommentAsync(Guid taskId, Guid commentId, bool trackChanges) =>
            await FindByCondition(e => e.TaskId.Equals(taskId) && e.Id.Equals(commentId), trackChanges).SingleOrDefaultAsync();
    }
}
