using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;

namespace TaskTracker.Contract
{
    public interface IAuthenticationService
    {
        Task<bool> IsValidUser(UserForAuthorizeDto userForAuth);

        Task<bool> IsValidNumber(string? number);

        string CreateToken();

        System.Threading.Tasks.Task SendMessageByBot(string? phoneNumber);
    }
}
