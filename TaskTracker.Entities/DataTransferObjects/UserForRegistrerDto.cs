using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Entities.DataTransferObjects
{
    public class UserForRegistrerDto : UserForManipulationDto
    {
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Должность обязательна для заполнения")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Никнем обязателен для заполнения")]
        public string UserName { get; set; }

        public string? Email { get; set; }
    }
}