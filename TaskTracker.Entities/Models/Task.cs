using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Entities.Models
{
    public class Task : BaseModel
    {
        [Required(ErrorMessage = "TaskName is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the description is 50 characters.")]
        public virtual string TaskName { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [MaxLength(180, ErrorMessage = "Maximum length for the description is 180 characters.")]
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
