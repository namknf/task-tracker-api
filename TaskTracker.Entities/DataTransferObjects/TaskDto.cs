namespace TaskTracker.Entities.DataTransferObjects
{
    public class TaskDto
    {
        public Guid Id { get; set; }

        public List<ParticipantDto> Participants { get; set; }

        public DateTime CreationDate { get; set; }

        public string Priority { get; set; }

        public string TaskName { get; set; }

        public string Status { get; set; }
    }
}
