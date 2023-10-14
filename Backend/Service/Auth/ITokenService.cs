using Backend.Model.DbEntities;

namespace Backend.Service.Auth
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user, string role);
    }
}
