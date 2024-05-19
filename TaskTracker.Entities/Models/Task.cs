using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Entities.Models
{
    public class Task : BaseModel
    {
        [Required(ErrorMessage = "Название задачи обязательно для заполнения.")]
        [MaxLength(50, ErrorMessage = "Максимальная длина название задачи 50 символов.")]
        public virtual string TaskName { get; set; }

        [Required(ErrorMessage = "Описание задачи обязательно для заполнения.")]
        [MaxLength(180, ErrorMessage = "Максимальная длина для описания 180 символов.")]
        public virtual string Description { get; set; }

        [ForeignKey(nameof(Status))]
        public virtual Guid TaskStatusId { get; set; }

        public virtual Status Status { get; set; }

        [ForeignKey(nameof(Priority))]
        public virtual Guid TaskPriorityId { get; set; }

        public virtual TaskPriority Priority { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public virtual DateTime Deadline { get; set; }

        public virtual List<User> Participants { get; set; }

        public virtual Project Project { get; set; }

        [ForeignKey(nameof(Project))]
        public virtual Guid ProjectId { get; set; }

        public virtual List<TaskAction>? Actions { get; set; }

        public virtual List<TaskComment>? Comments { get; set; }
    }
}
