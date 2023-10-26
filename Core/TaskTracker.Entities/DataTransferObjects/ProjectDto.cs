namespace TaskTracker.Entities.DataTransferObjects
{
    public class ProjectDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime CreationDate { get; set; }

        public List<ParticipantDto> Participants { get; set; }
    }
}
