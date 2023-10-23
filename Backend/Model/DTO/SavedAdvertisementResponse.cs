using IdentityTest.Model.DbEntities;

namespace IdentityTest.Model.DTO
{
    public class SavedAdvertisementResponse
    {
        public Guid Id { get; init; }
        public string JobType { get; init; }
        public string Advertiser { get; init; }
    }
}
