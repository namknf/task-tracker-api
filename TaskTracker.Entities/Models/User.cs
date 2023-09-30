using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Entities.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "FirstName is a required field.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is a required field.")]
        public string LastName { get; set; }

        public string Photo { get; set; }

        public IEnumerable<Task> Tasks { get; set; }

        public IEnumerable<Project> Projects { get; set; }
    }
}
