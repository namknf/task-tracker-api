using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Entities.DataTransferObjects
{
    public abstract class UserForManipulationDto
    {
        [Required(ErrorMessage = "Password name is required")]
        public string Password { get; set; }

        public string Email { get; set; }
    }
}
