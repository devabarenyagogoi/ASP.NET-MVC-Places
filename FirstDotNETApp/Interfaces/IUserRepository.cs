using FirstDotNETApp.Models;

namespace FirstDotNETApp.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByUsernameAndPassword(
            string username,
            string passwordHash);

        void SaveToken(UserToken userToken);

        void DeactivateToken(string token);

        UserToken? GetToken(string token);
    }
}