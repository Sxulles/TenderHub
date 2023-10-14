using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Model.DbEntities
{
    public class Surface
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public int X { get; set; }

        [Required]
        public int Y { get; set; }

        [Required]
        public string Unit { get; set; }

        [Required]
        [JsonIgnore]
        public Guid? JobTaskId { get; set; }

        [Required]
        [ForeignKey(nameof(JobTaskId))]
        [JsonIgnore]
        public Jobtask? Jobtask { get; set; }
    }
}
