using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Entities.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "FirstName is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the first name is 30 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the last name is 30 characters.")]
        public string LastName { get; set; }

        [MaxLength(30, ErrorMessage = "Maximum length for the position is 30 characters.")]
        public string Position { get; set; }

        [ForeignKey(nameof(Photo))]
        public Guid? PhotoId { get; set; }

        public File? Photo { get; set; }

        public string EmailCode { get; set; }

        public IEnumerable<Task>? Tasks { get; set; }

        public IEnumerable<Project>? Projects { get; set; }
    }
}
