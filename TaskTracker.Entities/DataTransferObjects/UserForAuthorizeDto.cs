using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Entities.DataTransferObjects
{
    public class UserForAuthorizeDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password name is required")]
        public string Password { get; set; }
    }
}
