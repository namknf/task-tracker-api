using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Entities.Models
{
    public class Project : BaseModel
    {
        [Required(ErrorMessage = "Название проекта обязательно для заполнения.")]
        [MaxLength(50, ErrorMessage = "Максимальная длина названия проекта 50 символов.")]
        public virtual string ProjectName { get; set; }

        public virtual DateTime Deadline { get; set; }

        public virtual DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Описание проекта обязательно для заполнения.")]
        [MaxLength(180, ErrorMessage = "Максимальная длина описания проекта 180 символов.")]
        public virtual string? Description { get; set; }

        public virtual List<User>? Participants { get; set; }

        public virtual List<Task>? Tasks { get; set; }
    }
}
