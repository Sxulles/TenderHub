using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Model.DbEntities
{
    public class Jobtask
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Surface? Surface { get; set; }

        [Required]
        public Guid? AdvertisementId { get; set; }

        [Required]
        [ForeignKey(nameof(AdvertisementId))]
        public Advertisement? Advertisement { get; set; }
    }
}
