namespace TaskTracker.Contract.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);

        string GenerateCode();

        Task SendQuestionAsync(string email, string subject, string message);
    }
}
