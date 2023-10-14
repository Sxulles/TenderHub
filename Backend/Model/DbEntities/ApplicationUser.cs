using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Backend.Model.DbEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        public string UserType { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public bool IsAdvertiser { get; set; }

        [Required]
        public ICollection<Advertisement>? Advertisements { get; set; }
    }
}
