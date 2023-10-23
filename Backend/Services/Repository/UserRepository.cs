using IdentityTest.Data;
using IdentityTest.Model;

namespace IdentityTest.Services.Repository
{
    public class UserRepository : IUserRepository
    {
        public ApplicationUser? GetUserByName(string username)
        {
            using var dbContext = new ApplicationDbContext();
            return dbContext.Users.FirstOrDefault(u => u.UserName == username);
        }
    }
}
