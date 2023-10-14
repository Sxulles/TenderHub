using Backend.Data;
using Backend.Model.DbEntities;

namespace Backend.Service.Repository
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
