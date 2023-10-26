namespace TaskTracker.Entities.DataTransferObjects
{
    public abstract class ProjectForManipulationDto
    {
        public string ProjectName { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime CreationDate { get => DateTime.Now; }

        public string Description { get; set; }

        public List<ParticipantDto> Participants { get; set; }
    }
}
