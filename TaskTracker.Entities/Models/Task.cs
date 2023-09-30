using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace TaskTracker.Entities.Models
{
    public class Task
    {
        [Column("TaskId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "TaskName is a required field.")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        public string Description { get; set; }

        [ForeignKey(nameof(Status))]
        public Guid TaskStatusId { get; set; }

        public Status Status { get; set; }

        [ForeignKey(nameof(Priority))]
        public Guid TaskPriorityId { get; set; }

        public TaskPriority Priority { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime Deadline { get; set; }

        public IEnumerable<User> Participants { get; set; }

        public Project Project { get; set; }

        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }

        public IEnumerable<TaskAction> Actions { get; set; }

        public IEnumerable<TaskComment> Comments { get; set; }
    }
}
