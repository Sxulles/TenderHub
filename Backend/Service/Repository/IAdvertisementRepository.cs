using Backend.Model.DbEntities;

namespace Backend.Service.Repository
{
    public interface IAdvertisementRepository
    {
        ICollection<Advertisement> GetAll();

        Task<bool> AddAdvertisement(Advertisement advertisement);
    }
}
