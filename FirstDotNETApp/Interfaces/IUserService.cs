using FirstDotNETApp.Models;

namespace FirstDotNETApp.Interfaces
{
    public interface IUserService
    {
        User? ValidateUser(string username, string password);
    }
}