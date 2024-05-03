using TaskTracker.Entities.DataTransferObjects;

namespace TaskTracker.Contract.Service
{
    public interface IAuthenticationService
    {
        Task<bool> IsValidUser(UserForAuthorizeDto userForAuth);

        string CreateToken(string userId);

        string CreateToken();
    }
}
