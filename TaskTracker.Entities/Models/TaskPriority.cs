using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Entities.Models
{
    public class TaskPriority
    {
        [Key]
        [Column("TaskPriorityId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "PriorityName is a required field.")]
        public string PriorityName { get; set; }
    }
}
