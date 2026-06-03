using FirstDotNETApp.Models;

namespace FirstDotNETApp.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByUsernameAndPassword(
            string username,
            string passwordHash);
    }
}