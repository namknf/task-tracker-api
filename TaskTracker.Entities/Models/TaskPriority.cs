using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Entities.Models
{
    public class TaskPriority : BaseModel
    {
        [Required(ErrorMessage = "Название приоритета задачи обязательно для заполнения.")]
        [MaxLength(15, ErrorMessage = "MМаксимальная длина названия приоритета задачи 15 символов.")]
        public virtual string PriorityName { get; set; }
    }
}
