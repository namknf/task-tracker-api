using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Entities.Models
{
    public class Project : BaseModel
    {
        [Required(ErrorMessage = "Project name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the project name is 50 characters.")]
        public virtual string ProjectName { get; set; }

        public virtual DateTime Deadline { get; set; }

        public virtual DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [MaxLength(180, ErrorMessage = "Maximum length for the description is 180 characters.")]
        public virtual string? Description { get; set; }

        public virtual List<User>? Participants { get; set; }

        public virtual List<Task>? Tasks { get; set; }
    }
}
