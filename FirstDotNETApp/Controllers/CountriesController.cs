using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstDotNETApp.Interfaces;
using FirstDotNETApp.ViewModels;

namespace FirstDotNETApp.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountryService _countryService;
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            return View(countries);
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View(new CountryViewModel());
        }

        // POST: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _countryService.CreateCountryAsync(vm);
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Countries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CountryViewModel vm)
        {
            if (id != vm.CountryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _countryService.UpdateCountryAsync(vm);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _countryService.CountryExistsAsync(vm.CountryId))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Countries/Delete/5
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

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _countryService.DeleteCountryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
