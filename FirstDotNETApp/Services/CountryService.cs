using FirstDotNETApp.Data;
using FirstDotNETApp.Models;
using FirstDotNETApp.Interfaces;
using FirstDotNETApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FirstDotNETApp.Services
{
    public class CountryService : ICountryService
    {
        private readonly AppDbContext _context;

        public CountryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CountryViewModel>> GetAllCountriesAsync()
        {
            return await _context.Countries.Select(
                c => new CountryViewModel
                {
                    CountryId = c.CountryId,
                    CountryName = c.CountryName,
                })
                .ToListAsync();
        }


        public async Task<CountryViewModel?> GetCountryByIdAsync(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(
                c => c.CountryId == id);

            if (country == null)
            {
                return null;
            }

            return new CountryViewModel
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName,
            };
        }

        public async Task CreateCountryAsync(CountryViewModel vm)
        {
            Country country = new Country
            {
                CountryId = vm.CountryId,
                CountryName = vm.CountryName,
            };
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCountryAsync(CountryViewModel vm) 
        {
            Country country = new Country
            {
                CountryId = vm.CountryId,
                CountryName = vm.CountryName
            };
            _context.Update(country);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCountryAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country != null)
            {
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CountryExistsAsync(int id)
        {
            return await _context.Countries.AnyAsync(c => c.CountryId == id);
        }
    }
}
