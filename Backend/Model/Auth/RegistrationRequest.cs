using System.ComponentModel.DataAnnotations;

namespace IdentityTest.Data
{
    public record RegistrationRequest(
    [Required] string Email,
    [Required] string Username,
    [Required] string Password
    );
}
