using FirstDotNETApp.Models;
using FirstDotNETApp.ViewModels;

namespace FirstDotNETApp.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryViewModel>> GetAllCountriesAsync();

        Task<CountryViewModel?> GetCountryByIdAsync(int id);

        Task CreateCountryAsync(CountryViewModel vm);

        Task UpdateCountryAsync(CountryViewModel vm);

        Task DeleteCountryAsync(int id);

        Task<bool> CountryExistsAsync(int id);
    }
}