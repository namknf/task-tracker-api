namespace TaskTracker.Contract
{
    public interface IRepositoryManager
    {
        ITaskRepository TaskRepository { get; }

        IProjectRepository ProjectRepository { get; }

        IStatusRepository StatusRepository { get; }

        IPriorityRepository PriorityRepository { get; }

        Task SaveAsync();

        void DetachAllEntities();
    }
}
