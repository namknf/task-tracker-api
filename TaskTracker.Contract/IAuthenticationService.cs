using TaskTracker.Entities.DataTransferObjects;

namespace TaskTracker.Contract
{
    public interface IAuthenticationService
    {
        Task<bool> ValidateUser(UserForAuthorizeDto userForAuth);

        Task<string> CreateToken();
    }
}
