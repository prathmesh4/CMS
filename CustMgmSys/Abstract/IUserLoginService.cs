using CustMgmSys.Models;

namespace CustMgmSys.Abstract
{
    public interface IUserLoginService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegistrationModel model);
    }
}
