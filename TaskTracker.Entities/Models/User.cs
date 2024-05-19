using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Entities.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Имя обязательно для заполнения.")]
        [MaxLength(30, ErrorMessage = "Максимальная длина для имени 30 символов.")]
        public virtual string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна для заполнения.")]
        [MaxLength(30, ErrorMessage = "Максимальная длина для фамилии 30 символов.")]
        public virtual string LastName { get; set; }

        [MaxLength(30, ErrorMessage = "Максимальная длина для должности 30 символов.")]
        public virtual string Position { get; set; }

        [ForeignKey(nameof(Photo))]
        public virtual Guid? PhotoId { get; set; }

        public virtual File? Photo { get; set; }

        public virtual string? EmailCode { get; set; }

        public virtual string? PasswordCode { get; set; }

        public virtual string? RefreshToken { get; set; }

        public virtual DateTime RefreshTokenExpiryTime { get; set; }

        public virtual List<Task>? Tasks { get; set; }

        public virtual List<Project>? Projects { get; set; }
    }
}
