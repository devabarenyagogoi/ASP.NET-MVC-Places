<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstDotNETApp.Data;
using FirstDotNETApp.Models;
<<<<<<< Updated upstream
=======
using FirstDotNETApp.ViewModels;
using FirstDotNETApp.Interfaces;
>>>>>>> Stashed changes

namespace FirstDotNETApp.Controllers
{
    public class CountriesController : Controller
    {
<<<<<<< Updated upstream
        private readonly AppDbContext _context;

        // Dependency Injection
        public CountriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries.ToListAsync());
        }

        // GET: Countries/Details/5
=======
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<IActionResult> Index()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            return View(countries);
        }

>>>>>>> Stashed changes
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

<<<<<<< Updated upstream
            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryId == id);
            if (country == null)
=======
            var vm = await _countryService.GetCountryByIdAsync(id.Value);

            if (vm == null)
>>>>>>> Stashed changes
            {
                return NotFound();
            }

            return View(vm);
        }

<<<<<<< Updated upstream
        // GET: Countries/Create
=======
>>>>>>> Stashed changes
        public IActionResult Create()
        {
            return View(new CountryViewModel());
        }

<<<<<<< Updated upstream
        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryId,CountryName")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
=======
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _countryService.CreateCountryAsync(vm);
>>>>>>> Stashed changes
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

<<<<<<< Updated upstream
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
=======
            var vm = await _countryService.GetCountryByIdAsync(id.Value);
            if (vm == null)
>>>>>>> Stashed changes
            {
                return NotFound();
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< Updated upstream
        public async Task<IActionResult> Edit(int id, [Bind("CountryId,CountryName")] Country country)
=======
        public async Task<IActionResult> Edit(int id, CountryViewModel vm)
>>>>>>> Stashed changes
        {
            if (id != vm.CountryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
<<<<<<< Updated upstream
                    _context.Update(country);
                    await _context.SaveChangesAsync();
=======
                    await _countryService.UpdateCountryAsync(vm);
>>>>>>> Stashed changes
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _countryService.CountryExistsAsync(vm.CountryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = await _countryService.GetCountryByIdAsync(id.Value);
            if (vm == null)
            {
                return NotFound();
            }
            return View(vm);
        }

<<<<<<< Updated upstream
        // POST: Countries/Delete/5
=======
>>>>>>> Stashed changes
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
<<<<<<< Updated upstream
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.CountryId == id);
        }
=======
            await _countryService.DeleteCountryAsync(id);
            return RedirectToAction(nameof(Index));
        }

>>>>>>> Stashed changes
    }
}
