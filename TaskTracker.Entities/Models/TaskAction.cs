using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Entities.Models
{
    public class TaskAction
    {
        [Key]
        [Column("TaskActionId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "ActionName is a required field.")]
        public string ActionName { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        public string Description { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime ActionDate { get; set; }

        [ForeignKey(nameof(Task))]
        public Guid TaskId { get; set; }

        public Task Task { get; set; }
    }
}
