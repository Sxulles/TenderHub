using System.ComponentModel.DataAnnotations;

namespace Backend.Model.Auth
{
    public record RegistrationRequest(
    [Required] string Email,
    [Required] string Username,
    [Required] string Password
    );
}
