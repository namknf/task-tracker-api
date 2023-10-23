using TaskTracker.Entities.DataTransferObjects;

namespace TaskTracker.Contract
{
    public interface IAuthenticationService
    {
        Task<bool> IsValidUser(UserForAuthorizeDto userForAuth);

        Task<bool> IsValidNumber(string? number);

        string CreateToken();

        Task SendMessageByBot(string? phoneNumber);
    }
}
