namespace TaskTracker.Entities.DataTransferObjects
{
    public class CommentDto
    {
        public Guid Id { get; set; }

        public string CommentText { get; set; }

        public UserInfoForCommentDto User { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
