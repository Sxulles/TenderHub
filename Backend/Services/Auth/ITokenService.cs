

using IdentityTest.Model;

namespace IdentityTest.Services.Auth
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user, string role);
    }
}
