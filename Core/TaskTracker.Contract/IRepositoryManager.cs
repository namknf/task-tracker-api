namespace TaskTracker.Contract
{
    public interface IRepositoryManager
    {
        ITaskRepository TaskRepository { get; }

        IProjectRepository ProjectRepository { get; }

        IUserRepository UserRepository { get; }

        Task SaveAsync();
    }
}
