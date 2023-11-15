namespace TaskTracker.Contract
{
    public interface IRepositoryManager
    {
        ITaskRepository TaskRepository { get; }

        IProjectRepository ProjectRepository { get; }

        IStatusRepository StatusRepository { get; }

        Task SaveAsync();

        void DetachAllEntities();
    }
}
