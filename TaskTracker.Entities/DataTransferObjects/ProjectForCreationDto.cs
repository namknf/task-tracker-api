namespace TaskTracker.Entities.DataTransferObjects
{
    public class ProjectForCreationDto : ProjectForManipulationDto
    {
        public DateTime CreationDate { get => DateTime.Now; }
    }
}
