using IdentityTest.Model.DbEntities;
using IdentityTest.Model.DTO;

namespace IdentityTest.Services.Repository
{
    public interface IAdvertisementRepository
    {
        ICollection<AdvertisementResponse> GetAll(string userName);

        Task<bool> AddAdvertisement(Advertisement advertisement);

        Task<bool> SaveAdvertisementForUser(string userName, Guid advertisementId);

        ICollection<SavedAdvertisementResponse> GetSavedAdvertisementForUser(string userName);

        Task<bool> DeleteAdvertisement(Guid savedAdvertisementId);
    }
}
