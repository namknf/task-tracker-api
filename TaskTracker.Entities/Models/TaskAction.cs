using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Entities.Models
{
    public class TaskAction : BaseModel
    {
        [Required(ErrorMessage = "ActionName is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the action name is 50 characters.")]
        public virtual string ActionName { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [MaxLength(180, ErrorMessage = "Maximum length for the description is 180 characters.")]
        public virtual string Description { get; set; }

        [ForeignKey(nameof(User))]
        public virtual string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual DateTime ActionDate { get; set; }

        [ForeignKey(nameof(Task))]
        public virtual Guid TaskId { get; set; }

        public virtual Task Task { get; set; }
    }
}
