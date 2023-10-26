using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Entities.Models
{
    public class TaskAction : BaseModel
    {
        [Required(ErrorMessage = "ActionName is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the action name is 50 characters.")]
        public string ActionName { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [MaxLength(180, ErrorMessage = "Maximum length for the description is 180 characters.")]
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
