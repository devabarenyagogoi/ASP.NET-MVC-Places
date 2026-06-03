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
    }
}