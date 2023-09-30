using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Entities.Models
{
    public class Status
    {
        [Column("StatusId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "StatusName is a required field.")]
        public string StatusName { get; set; }
    }
}
