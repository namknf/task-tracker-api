namespace TaskTracker.Entities.DataTransferObjects
{
    public abstract class ProjectForManipulationDto
    {
        public string ProjectName { get; set; }

        public DateTime Deadline { get; set; }

        public string Description { get; set; }
    }
}
