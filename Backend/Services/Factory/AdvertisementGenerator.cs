using Backend.Model.DbEntities;

namespace Backend.Services.Factory
{
    public class AdvertisementGenerator
    {
        public static Advertisement CreateAdvertisement(Guid userId)
        {
            var advertisement = new Advertisement
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = userId,
                DeadlineStart = DateTime.UtcNow,
                DeadlineEnd = DateTime.UtcNow.AddDays(6),
                JobType = "TestJob",
                IsHighlighted = Random.Shared.Next(0, 1) == 0
            };

            advertisement.Location = CreateLocation(advertisement.Id);
            advertisement.Jobtasks = CreateJobtasks(advertisement.Id, 2).ToList();

            return advertisement;
        }

        private static Location CreateLocation(Guid AdvertisementId)
        {
            var location = new Location
            {
                Id = Guid.NewGuid(),
                Country = "Country",
                County = "County",
                City = "City",
                District = "District",
                AdvertisementId = AdvertisementId
            };

            return location;
        }

        private static Surface CreateSurface(Guid jobtaskId)
        {
            var surface = new Surface
            {
                Id = Guid.NewGuid(),
                X = 10,
                Y = 10,
                Unit = "m2",
                JobTaskId = jobtaskId,
            };

            return surface;
        }

        private static IEnumerable<Jobtask> CreateJobtasks(Guid advertisementId, uint quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                var jobtask = new Jobtask
                {
                    Id = Guid.NewGuid(),
                    AdvertisementId = advertisementId,
                    Description = "desc",
                };
                jobtask.Surface = CreateSurface(jobtask.Id);

                yield return jobtask;
            }
        }
    }
}
