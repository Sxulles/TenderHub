using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Model.DbEntities
{
    public class Location
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public string Country { get; init; }

        [Required]
        public string County { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string? District { get; set; }

        [Required]
        [JsonIgnore]
        public Guid? AdvertisementId { get; set; }

        [Required]
        [ForeignKey(nameof(AdvertisementId))]
        [JsonIgnore]
        public Advertisement? Advertisement { get; set; }
    }
}
