namespace TaskTracker.Entities.DataTransferObjects
{
    public class ProjectForGetDto : ProjectDto
    {
        public ProjectTasksInfo TasksInfo { get; set; }
    }
}
