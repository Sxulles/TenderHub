using IdentityTest.Model.DbEntities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityTest.Model
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
        public ICollection<SavedAdvertisement>? SavedAdvertisements { get; set; }
    }
}
