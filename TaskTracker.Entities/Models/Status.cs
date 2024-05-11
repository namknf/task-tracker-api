using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Entities.Models
{
    public class Status : BaseModel
    {
        [Required(ErrorMessage = "StatusName is a required field.")]
        [MaxLength(15, ErrorMessage = "Maximum length for the status name is 15 characters.")]
        public virtual string StatusName { get; set; }
    }
}
