using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdentityTest.Model.DbEntities
{
    public class SavedAdvertisement
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid ApplicationUserId { get; set; }
        [Required]
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public Guid AdvertisementId { get; set; }
        [Required]
        [ForeignKey(nameof(AdvertisementId))]
        public Advertisement Advertisement { get; set; }
    }
}
