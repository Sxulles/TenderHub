namespace Backend.Model.Auth
{
    public record AuthResponse(string Token, string Username, bool Success);
}
