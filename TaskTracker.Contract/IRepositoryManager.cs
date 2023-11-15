namespace TaskTracker.Contract
{
    public interface IRepositoryManager
    {
        ITaskRepository TaskRepository { get; }

        IProjectRepository ProjectRepository { get; }

        Task SaveAsync();

        void DetachAllEntities();
    }
}
