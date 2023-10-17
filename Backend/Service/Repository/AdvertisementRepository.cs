using Backend.Data;
using Backend.Model.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Service.Repository
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        public ICollection<Advertisement> GetAll()
        {
            using var dbContext = new ApplicationDbContext();
            return dbContext.Advertisements
                .Include(a => a.ApplicationUser)
                .Include(a => a.Location)
                .Include(a => a.Jobtasks).ThenInclude(j => j.Surface)
                .OrderByDescending(a => a.IsHighlighted)
                .ToList();
        }

        public async Task<bool> AddAdvertisement(Advertisement advertisement)
        {
            if (advertisement != null)
            {
                using var dbContext = new ApplicationDbContext();
                dbContext.Advertisements.Add(advertisement);
                await dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
