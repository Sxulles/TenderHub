using IdentityTest.Model;
using Microsoft.AspNetCore.Identity;

namespace IdentityTest.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        #region RegisterAsync
        public async Task<AuthResult> RegisterAsync(string email, string username, string password, string role, string userType)
        {
            var user = new ApplicationUser { UserName = username, Email = email, CompanyName = "Company", UserType=userType};
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return FailedRegistration(result, email, username);
            }

            await _userManager.AddToRoleAsync(user, role); // Adding the user to a role
            return new AuthResult(true, email, username, "");
        }

        private static AuthResult FailedRegistration(IdentityResult result, string email, string username)
        {
            var authResult = new AuthResult(false, email, username, "");

            foreach (var error in result.Errors)
            {
                authResult.AddError(error.Code, error.Description);
            }

            return authResult;
        }
        #endregion

        #region LoginAsync
        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return IncompleteCredentials(email, password);
            }

            var managedUser = await _userManager.FindByEmailAsync(email);

            if (managedUser == null)
            {
                return InvalidEmail(email);
            }

            var userRole = await _userManager.GetRolesAsync(managedUser);

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, password);

            if (!isPasswordValid)
            {
                if (managedUser.UserName != null) return InvalidPassword(email, managedUser.UserName);
            }

            var accessToken = _tokenService.CreateToken(managedUser, userRole[0]);

            return new AuthResult(true, managedUser.Email, managedUser.UserName, accessToken);
        }
        private static AuthResult InvalidEmail(string email)
        {
            var result = new AuthResult(false, email, "", "");
            result.AddError("401 Bad credentials", $"Invalid email: {email}");
            return result;
        }

        private static AuthResult InvalidPassword(string email, string userName)
        {
            var result = new AuthResult(false, email, userName, "");
            result.AddError("401 Bad credentials", $"Invalid password: {userName}");
            return result;
        }

        private static AuthResult IncompleteCredentials(string email, string password)
        {
            var result = new AuthResult(false, email, "", "");
            result.AddError("401 Bad credentials", $"Incomplete credentials: {email}-{password}");
            return result;
        }
        #endregion
    }
}
