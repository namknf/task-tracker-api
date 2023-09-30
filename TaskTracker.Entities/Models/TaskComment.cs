using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Entities.Models
{
    public class TaskComment
    {
        [Column("TaskCommentId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "CommentText is a required field.")]
        public string CommentText { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User User { get; set; }

        [ForeignKey(nameof(Task))]
        public Task Task { get; set; }

        public Guid TaskId { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
