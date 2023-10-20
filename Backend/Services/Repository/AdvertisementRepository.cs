using Backend.Data;
using Backend.Model.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Repository
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

        public async Task<bool> SaveAdvertisementForUser(string userName, Guid advertismentId)
        {
            try
            {
                using var dbContext = new ApplicationDbContext();
                var advertisement = dbContext.Advertisements.FirstOrDefault(a => a.Id == advertismentId);
                var user = dbContext.Users.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
                advertisement.ApplicationUserId = user.Id;


                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ICollection<Advertisement> GetSavedAdvertisementForUser(string userName)
        {
            using var dbContext = new ApplicationDbContext();
            return dbContext.Advertisements.Where(a => a.ApplicationUser.UserName == userName).ToList();
        }

        public bool DeleteSavedAdvertisement(string username)
        {
            try
            {
                using var dbContext = new ApplicationDbContext();
                var advertisement = dbContext.Advertisements.FirstOrDefault(a => a.ApplicationUser.UserName == username);
                dbContext.Users.FirstOrDefault(u => u.UserName == username).Advertisements.Remove(advertisement);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
