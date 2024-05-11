using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Entities.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "FirstName is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the first name is 30 characters.")]
        public virtual string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the last name is 30 characters.")]
        public virtual string LastName { get; set; }

        [MaxLength(30, ErrorMessage = "Maximum length for the position is 30 characters.")]
        public virtual string Position { get; set; }

        [ForeignKey(nameof(Photo))]
        public virtual Guid? PhotoId { get; set; }

        public virtual File? Photo { get; set; }

        public virtual string? EmailCode { get; set; }

        public virtual string? RefreshToken { get; set; }

        public virtual DateTime RefreshTokenExpiryTime { get; set; }

        public virtual List<Task>? Tasks { get; set; }

        public virtual List<Project>? Projects { get; set; }
    }
}
