namespace TaskTracker.Entities.DataTransferObjects
{
    public abstract class TaskForManipulationDto
    {
        public string TaskName { get; set; }

        public DateTime Deadline { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public string Description { get; set; }

        public List<ParticipantDto> Participants { get; set; }

        public virtual Guid TaskStatusId { get; set; }

        public Guid TaskPriorityId { get; set; }
    }
}
