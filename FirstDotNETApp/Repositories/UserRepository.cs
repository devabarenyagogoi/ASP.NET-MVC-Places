using FirstDotNETApp.Data;
using FirstDotNETApp.Interfaces;
using FirstDotNETApp.Models;

namespace FirstDotNETApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User? GetUserByUsernameAndPassword(
            string username,
            string passwordHash)
        {
            return _context.Users.FirstOrDefault(u =>
                u.Username == username &&
                u.PasswordHash == passwordHash &&
                u.IsActive);
        }

        public void SaveToken(UserToken userToken)
        {
            _context.UserTokens.Add(userToken);
            _context.SaveChanges();
        }

        public void DeactivateToken(string token)
        {
            var userToken = _context.UserTokens
                .FirstOrDefault(t => t.Token == token);

            if (userToken != null)
            {
                userToken.IsActive = false;
                _context.SaveChanges();
            }
        }
        public UserToken? GetToken(string token)
        {
            return _context.UserTokens
                .FirstOrDefault(t => t.Token == token);
        }
    }
}