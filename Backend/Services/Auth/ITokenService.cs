using Backend.Model.DbEntities;

namespace Backend.Services.Auth
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user, string role);
    }
}
