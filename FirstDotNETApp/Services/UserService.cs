using FirstDotNETApp.Interfaces;
using FirstDotNETApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace FirstDotNETApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? ValidateUser(string username, string password)
        {
            string hashedPassword = HashPassword(password);

            return _userRepository.GetUserByUsernameAndPassword(
                username,
                hashedPassword);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);

            return Convert.ToHexString(hash).ToLower();
        }

        public string GenerateToken(string username)
        {
            string tokenData =
                $"{username}|{DateTime.Now:yyyyMMddHHmmssfff}";

            using var sha256 = SHA256.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(tokenData);
            byte[] hash = sha256.ComputeHash(bytes);

            return Convert.ToHexString(hash).ToLower();
        }

        public bool IsTokenValid(string token)
        {
            var tokenRecord = _userRepository.GetToken(token);

            if (tokenRecord == null)
                return false;

            if (!tokenRecord.IsActive)
                return false;

            if (tokenRecord.ExpiryAt <= DateTime.Now)
                return false;

            return true;
        }

        public string? ExtractBearerToken(string? authHeader)
        {
            if (string.IsNullOrWhiteSpace(authHeader))
                return null;

            var parts = authHeader.Split(' ');

            if (parts.Length != 2)
                return null;

            if (!parts[0].Equals("Bearer",
                StringComparison.OrdinalIgnoreCase))
                return null;

            return parts[1];
        }
    }
}