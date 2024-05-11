using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Entities.Models
{
    public class TaskComment : BaseModel
    {
        [Required(ErrorMessage = "CommentText is a required field.")]
        [MaxLength(180, ErrorMessage = "Maximum length for the comment text is 180 characters.")]
        public virtual string CommentText { get; set; }

        [ForeignKey(nameof(User))]
        public virtual string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Task Task { get; set; }

        [ForeignKey(nameof(Task))]
        public virtual Guid TaskId { get; set; }

        public virtual DateTime CreationDate { get; set; }
    }
}
