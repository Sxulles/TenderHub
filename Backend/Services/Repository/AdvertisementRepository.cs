using IdentityTest.Data;
using IdentityTest.Model.DbEntities;
using IdentityTest.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace IdentityTest.Services.Repository
{
    public class AdvertisementRepository : IAdvertisementRepository 
    {
        public ICollection<AdvertisementResponse> GetAll(string userName)
        {
            using var dbContext = new ApplicationDbContext();
            var advertisements = dbContext.Advertisements
                .Include(a => a.ApplicationUser)
                .Include(a => a.Location)
                .Include(a => a.Jobtasks).ThenInclude(j => j.Surface)
                .OrderByDescending(a => a.IsHighlighted)
                .ToList();


            var savedAdvertisementsOfUser = GetSavedAdvertisementForUser(userName).Select(s => s.Id);


            return advertisements.Select(advertisement => new AdvertisementResponse
            {
                Id = advertisement.Id,
                Status = advertisement.Status,
                JobType = advertisement.JobType,
                IsHighlighted = advertisement.IsHighlighted,
                Jobtasks = advertisement.Jobtasks,
                Location = advertisement.Location,
                DeadlineStart = advertisement.DeadlineStart,
                DeadlineEnd = advertisement.DeadlineEnd,
                Advertiser = advertisement.ApplicationUser?.CompanyName,
                IsSavedByUser = savedAdvertisementsOfUser.Contains(advertisement.Id)
            }).ToList();
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
            using var dbContext = new ApplicationDbContext();

            var userId = dbContext.Users.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower()).Id;

            var savedAdvertisement = new SavedAdvertisement
            {
                ApplicationUserId = userId,
                AdvertisementId = advertismentId
            };

            bool isAlreadySaved = dbContext.SavedAdvertisements.Any(s => s.AdvertisementId == advertismentId && s.ApplicationUserId == userId);

            if (!isAlreadySaved)
            {
                dbContext.SavedAdvertisements.Add(savedAdvertisement);
                await dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public ICollection<SavedAdvertisementResponse> GetSavedAdvertisementForUser(string userName)
        {
            using var dbContext = new ApplicationDbContext();
            var savedAdvertisements = dbContext.SavedAdvertisements
                .Where(s => s.ApplicationUser.UserName == userName)
                .Include(s => s.Advertisement)
                .Include(s => s.ApplicationUser)
                .ToList();

            return savedAdvertisements.Select(sa => new SavedAdvertisementResponse
            {
                Id = sa.Id,
                JobType = sa.Advertisement.JobType,
                Advertiser = sa.ApplicationUser.CompanyName
            }).ToList();
        }

        public async Task<bool> DeleteAdvertisement(Guid savedAdvertisementId)
        {
            try
            {
                using var dbContext = new ApplicationDbContext();
                dbContext.SavedAdvertisements.Remove(dbContext.SavedAdvertisements.FirstOrDefault(s => s.Id == savedAdvertisementId));
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
