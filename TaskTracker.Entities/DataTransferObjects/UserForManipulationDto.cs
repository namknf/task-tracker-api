using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Entities.DataTransferObjects
{
    public abstract class UserForManipulationDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password name is required")]
        public string Password { get; set; }
    }
}
