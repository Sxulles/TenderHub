using Backend.Model.DbEntities;

namespace Backend.Service.Repository
{
    public interface IUserRepository
    {
        ApplicationUser? GetUserByName(string username);
    }
}
