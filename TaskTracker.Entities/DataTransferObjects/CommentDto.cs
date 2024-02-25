namespace TaskTracker.Entities.DataTransferObjects
{
    public class CommentDto
    {
        public Guid Id { get; set; }

        public string CommentText { get; set; }

        public string UserId { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
