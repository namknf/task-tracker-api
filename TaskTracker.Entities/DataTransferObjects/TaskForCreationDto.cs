namespace TaskTracker.Entities.DataTransferObjects
{
    public class TaskForCreationDto : TaskForManipulationDto
    {
        public override DateTime CreationDate { get => DateTime.Now; }
    }
}
