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
    }
}