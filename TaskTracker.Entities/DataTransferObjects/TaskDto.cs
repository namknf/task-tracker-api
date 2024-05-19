namespace TaskTracker.Entities.DataTransferObjects
{
    public class TaskDto : TaskForManipulationDto
    {
        public Guid Id { get; set; }

        public List<ParticipantForGetDto> Participants { get; set; }

        public DateTime CreationDate { get; set; }

        public PriorityDto Priority { get; set; }

        public StatusDto Status { get; set; }
    }
}
