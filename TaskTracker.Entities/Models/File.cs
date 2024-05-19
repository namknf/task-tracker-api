namespace TaskTracker.Entities.Models
{
    public class File : BaseModel
    {
        public virtual string FileName { get; set; }

        public virtual string Title { get; set; }

        public virtual byte[] Data { get; set; }
    }
}
