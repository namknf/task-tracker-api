namespace TaskTracker.Entities.DataTransferObjects
{
    public class ProjectDto
    {
        public Guid Id { get; set; }

        public string ProjectName { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime CreationDate { get; set; }

        public string? Description { get; set; }

        public List<ParticipantDto> Participants { get; set; }
    }
}
