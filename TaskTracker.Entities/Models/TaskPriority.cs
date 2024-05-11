using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Entities.Models
{
    public class TaskPriority : BaseModel
    {
        [Required(ErrorMessage = "PriorityName is a required field.")]
        [MaxLength(15, ErrorMessage = "Maximum length for the task priority is 15 characters.")]
        public virtual string PriorityName { get; set; }
    }
}
