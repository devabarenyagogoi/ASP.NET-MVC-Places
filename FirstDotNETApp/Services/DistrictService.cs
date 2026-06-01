using FirstDotNETApp.Data;
using FirstDotNETApp.Interfaces;
using FirstDotNETApp.Models;
using FirstDotNETApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FirstDotNETApp.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly AppDbContext _context;

        public DistrictService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DistrictViewModel>> GetAllDistrictsAsync()
        {
            return await _context.Districts
                .Include(d => d.State)
                .Select(d => new DistrictViewModel
                {
                    DistrictId = d.DistrictId,
                    DistrictName = d.DistrictName,
                    StateId = d.StateId,
                    StateName = d.State.StateName
                }).ToListAsync();
        }

        public async Task<DistrictViewModel?> GetDistrictByIdAsync(int id)
        {
            var district = await _context.Districts
                .Include(d => d.State)
                .FirstOrDefaultAsync(d => d.DistrictId == id);

            if (district == null)
            {
                return null;
            }

            return new DistrictViewModel
            {
                DistrictId = district.DistrictId,
                DistrictName = district.DistrictName,
                StateId = district.StateId,
                StateName = district.State?.StateName
            };
        }

        public async Task CreateDistrictAsync(DistrictViewModel vm)
        {
            District district = new District
            {
                DistrictId = vm.DistrictId,
                DistrictName = vm.DistrictName,
                StateId = vm.StateId
            };
            _context.Districts.Add(district);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDistrictAsync(DistrictViewModel vm)
        {

            District district = new District 
            {
                DistrictId = vm.DistrictId,
                DistrictName = vm.DistrictName,
                StateId = vm.StateId
            };

            _context.Update(district);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDistrictAsync(int id)
        {
            var district = await _context.Districts.FindAsync(id);
            
            if (district != null)
            {
                _context.Districts.Remove(district);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DistrictExistsAsync(int id)
        {
            return await _context.Districts
                .AnyAsync(e => e.DistrictId == id);
        }
    }
}
