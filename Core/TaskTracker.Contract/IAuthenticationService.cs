using TaskTracker.Entities.DataTransferObjects;

namespace TaskTracker.Contract
{
    public interface IAuthenticationService
    {
        Task<bool> IsValidUser(UserForAuthorizeDto userForAuth);

        string CreateToken();
    }
}
