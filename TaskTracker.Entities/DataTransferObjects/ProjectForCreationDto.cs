namespace TaskTracker.Entities.DataTransferObjects
{
    public class ProjectForCreationDto : ProjectForManipulationDto
    {
        public override DateTime CreationDate { get => DateTime.Now; }
    }
}
