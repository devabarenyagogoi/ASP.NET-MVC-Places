using FirstDotNETApp.Models;

namespace FirstDotNETApp.Interfaces
{
    public interface IUserService
    {
        User? ValidateUser(string username, string password);

        string GenerateToken(string username);

        bool IsTokenValid(string token);

        string? ExtractBearerToken(string? authHeader);
    }
}