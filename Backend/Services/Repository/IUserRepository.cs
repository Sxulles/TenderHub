using Backend.Model.DbEntities;

namespace Backend.Services.Repository
{
    public interface IUserRepository
    {
        ApplicationUser? GetUserByName(string username);
    }
}
