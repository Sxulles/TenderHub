using Backend.Model.Auth;

namespace Backend.Service.Auth
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(string email, string username, string password, string role);
        Task<AuthResult> LoginAsync(string email, string password);
    }
}
