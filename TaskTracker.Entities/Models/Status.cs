using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Entities.Models
{
    public class Status : BaseModel
    {
        [Required(ErrorMessage = "Название статуса задачи является обязательным для заполнения.")]
        [MaxLength(15, ErrorMessage = "Максимальная длина для названия статуса 15 символов.")]
        public virtual string StatusName { get; set; }
    }
}
