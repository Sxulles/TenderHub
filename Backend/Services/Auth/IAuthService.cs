namespace IdentityTest.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(string email, string username, string password, string role, string userType);
        Task<AuthResult> LoginAsync(string email, string password);
    }
}
