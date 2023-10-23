using IdentityTest.Model.DbEntities;

namespace IdentityTest.Model.DTO
{
    public class AdvertisementResponse
    {
        public Guid Id { get; init; }
        public string Status { get; init; }
        public string JobType { get; init; }
        public bool IsHighlighted { get; init; }
        public ICollection<Jobtask>? Jobtasks { get; init; }
        public Location Location { get; init; }
        public DateTime DeadlineStart { get; init; }
        public DateTime DeadlineEnd { get; init; }
        public string Advertiser { get; init; }
        public bool IsSavedByUser { get; set; }
    }
}
