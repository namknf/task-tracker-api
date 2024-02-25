using TaskTracker.Entities.Models;
using TaskTracker.Entities.RequestFeatures.Entities;
using TaskTracker.Entities.RequestFeatures;

namespace TaskTracker.Contract.Repository
{
    public interface ICommentRepository
    {
        void CreateComment(TaskComment comment);

        void DeleteComment(TaskComment comment);

        void UpdateComment(TaskComment comment);

        Task<PagedList<TaskComment>> GetAllCommentsForTaskAsync(Guid taskId, bool trackChanges, CommentParameters parms);

        Task<TaskComment?> GetCommentAsync(Guid taskId, Guid commentId, bool trackChanges);
    }
}
