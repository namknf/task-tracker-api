namespace TaskTracker.Entities.DataTransferObjects
{
    public class UserDto
    {
        public string FirstName { get; set; }

        public string Email { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public List<TaskDto>? Tasks { get; set; }

        public List<ProjectDto>? Projects { get; set; }
        
        public ImageDto? Photo { get; set; }
    }
}
