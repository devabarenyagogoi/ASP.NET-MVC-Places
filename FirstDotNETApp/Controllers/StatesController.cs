using FirstDotNETApp.Filters;
using FirstDotNETApp.Interfaces;
using FirstDotNETApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FirstDotNETApp.Controllers
{
    [TokenAuthorize]
    public class StatesController : Controller
    {
        private readonly IStateService _stateService;
        private readonly ICountryService _countryService;

        public StatesController(IStateService stateService, ICountryService countryService)
        {
            _stateService = stateService;
            _countryService = countryService;
        }

        // GET: States
        public async Task<IActionResult> Index()
        {
            var items = await _stateService.GetAllStatesAsync();
            return View(items);
        }

        // GET: States/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var vm = await _stateService.GetStateByIdAsync(id.Value);

            if (vm == null) return NotFound();

            return View(vm);
        }

        // GET: States/Create
        public async Task<IActionResult> Create()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            ViewData["CountryId"] = new SelectList(countries, "CountryId", "CountryName");
            return View(new StateViewModel());
        }

        // POST: States/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _stateService.CreateStateAsync(vm);
                return RedirectToAction(nameof(Index));
            }

            var countries = await _countryService.GetAllCountriesAsync();
            ViewData["CountryId"] = new SelectList(countries, "CountryId", "CountryName", vm.CountryId);
            return View(vm);
        }

        // GET: States/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var vm = await _stateService.GetStateByIdAsync(id.Value);

            if (vm == null) return NotFound();

            var countries = await _countryService.GetAllCountriesAsync();
            ViewData["CountryId"] = new SelectList(countries, "CountryId", "CountryName", vm.CountryId);
            return View(vm);
        }

        // POST: States/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StateViewModel vm)
        {
            if (id != vm.StateId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _stateService.UpdateStateAsync(vm);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _stateService.StateExistsAsync(vm.StateId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var countries = await _countryService.GetAllCountriesAsync();
            ViewData["CountryId"] = new SelectList(countries, "CountryId", "CountryName", vm.CountryId);
            return View(vm);
        }

        // GET: States/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var vm = await _stateService.GetStateByIdAsync(id.Value);

            if (vm == null) return NotFound();

            return View(vm);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _stateService.DeleteStateAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}