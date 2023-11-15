namespace TaskTracker.Entities.DataTransferObjects
{
    public class TaskForUpdateDto : TaskForManipulationDto
    {
        public Guid TaskStatusId { get; set; }
    }
}
