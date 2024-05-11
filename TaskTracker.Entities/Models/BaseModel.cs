using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Entities.Models
{
    public abstract class BaseModel
    {
        [Key]
        public virtual Guid Id { get; set; }
    }
}
