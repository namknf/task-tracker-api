using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Entities.Models
{
    public class Project
    {
        [Column("ProjectId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "ProjectName is a required field.")]
        public string ProjectName { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        public string Description { get; set; }

        public IEnumerable<User> Participants { get; set; }

        public IEnumerable<Task> Tasks { get; set; }
    }
}
