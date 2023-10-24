using TaskTracker.Entities.Models;

namespace TaskTracker.Contract
{
    public interface ICodeAttemptRepository
    {
        void CreateCodeAttempt(CodeAttempt attempt);

        Task<CodeAttempt?> GetCodeAttemptAsync(string phoneNumber, string code, bool trackChanges);
    }
}
