using Backend.Model.DbEntities;

namespace Backend.Services.Repository
{
    public interface IAdvertisementRepository
    {
        ICollection<Advertisement> GetAll();

        Task<bool> AddAdvertisement(Advertisement advertisement);

        Task<bool> SaveAdvertisementForUser(string userName, Guid advertisementId);

        ICollection<Advertisement> GetSavedAdvertisementForUser(string userName);
    }
}
