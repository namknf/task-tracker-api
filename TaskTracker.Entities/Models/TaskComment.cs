using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Entities.Models
{
    public class TaskComment : BaseModel
    {
        [Required(ErrorMessage = "CommentText is a required field.")]
        [MaxLength(180, ErrorMessage = "Maximum length for the comment text is 180 characters.")]
        public string CommentText { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public User User { get; set; }

        public Task Task { get; set; }

        [ForeignKey(nameof(Task))]
        public Guid TaskId { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
