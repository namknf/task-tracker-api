namespace TaskTracker.Entities.DataTransferObjects
{
    public class ProjectTasksInfo
    {
        public int AllTasks { get; set; }

        public int DoneTasks { get; set; }

        public int FrozenTasks { get; set; }

        public int NewTasks { get; set; }
    }
}
