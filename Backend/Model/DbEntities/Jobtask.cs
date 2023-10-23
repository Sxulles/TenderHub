using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IdentityTest.Model.DbEntities
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
        [JsonIgnore]
        public Guid? AdvertisementId { get; set; }

        [Required]
        [ForeignKey(nameof(AdvertisementId))]
        [JsonIgnore]
        public Advertisement? Advertisement { get; set; }
    }
}
