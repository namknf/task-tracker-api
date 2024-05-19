using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Entities.Models
{
    public class TaskComment : BaseModel
    {
        [Required(ErrorMessage = "Текст комментария обязателен для заполнения.")]
        [MaxLength(180, ErrorMessage = "Макисмальная длина текста комментария 180 символов.")]
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
