using FirstDotNETApp.Data;
using FirstDotNETApp.Interfaces;
using FirstDotNETApp.Models;
using FirstDotNETApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FirstDotNETApp.Services
{
    public class StateService : IStateService
    {
        private readonly AppDbContext _context;

        public StateService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StateViewModel>> GetAllStatesAsync()
        {
            return await _context.States
                .Include(s => s.Country)
                .Select(s => new StateViewModel
                {
                    StateId = s.StateId,
                    StateName = s.StateName,
                    CountryId = s.CountryId,
                    CountryName = s.Country.CountryName
                })
                .ToListAsync();
        }

        public async Task<StateViewModel?> GetStateByIdAsync(int id)
        {
            var state = await _context.States
                .Include(s => s.Country)
                .FirstOrDefaultAsync(s => s.StateId == id);

            if (state == null)
                return null;

            return new StateViewModel
            {
                StateId = state.StateId,
                StateName = state.StateName,
                CountryId = state.CountryId,
                CountryName = state.Country?.CountryName
            };
        }

        public async Task CreateStateAsync(StateViewModel vm)
        {
            State state = new State
            {
                StateId = vm.StateId,
                StateName = vm.StateName,
                CountryId = vm.CountryId
            };

            _context.States.Add(state);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStateAsync(StateViewModel vm)
        {
            State state = new State
            {
                StateId = vm.StateId,
                StateName = vm.StateName,
                CountryId = vm.CountryId
            };

            _context.Update(state);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStateAsync(int id)
        {
            var state = await _context.States.FindAsync(id);

            if (state != null)
            {
                _context.States.Remove(state);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> StateExistsAsync(int id)
        {
            return await _context.States
                .AnyAsync(s => s.StateId == id);
        }
    }
}