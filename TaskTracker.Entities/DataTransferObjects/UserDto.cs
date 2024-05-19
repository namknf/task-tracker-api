namespace TaskTracker.Entities.DataTransferObjects
{
    public class UserDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string UserName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public List<TaskDto>? Tasks { get; set; }

        public List<ProjectDto>? Projects { get; set; }
        
        public Guid? PhotoId { get; set; }

        public int InProgressTasks { get; set; }

        public int ClosedTasks { get; set; }

        public int FrozenTasks { get; set; }
    }
}
