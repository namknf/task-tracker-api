namespace TaskTracker.Contract
{
    public interface IRepositoryManager
    {
        ITaskRepository TaskRepository { get; }

        IProjectRepository ProjectRepository { get; }

        ICodeAttemptRepository CodeAttemptRepository { get; }

        Task SaveAsync();
    }
}
