using IdentityTest.Model;

namespace IdentityTest.Services.Repository
{
    public interface IUserRepository
    {
        ApplicationUser? GetUserByName(string username);
    }
}
