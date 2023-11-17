namespace TaskTracker.Entities.DataTransferObjects
{
    public class TaskDto
    {
        public Guid Id { get; set; }

        public List<ParticipantForGetDto> Participants { get; set; }

        public DateTime CreationDate { get; set; }

        public PriorityDto Priority { get; set; }

        public string TaskName { get; set; }

        public StatusDto Status { get; set; }
    }
}
