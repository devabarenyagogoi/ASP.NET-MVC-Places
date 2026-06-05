using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FirstDotNETApp.Interfaces;
using FirstDotNETApp.Models;
using FirstDotNETApp.ViewModels;
using FirstDotNETApp.Filters;

namespace FirstDotNETApp.Controllers
{
    [TokenAuthorize]
    public class DistrictsController : Controller
    {
        private readonly IDistrictService _districtService;
        private readonly IStateService _stateService;

        public DistrictsController(
            IDistrictService districtService,
            IStateService stateService)
        {
            _districtService = districtService;
            _stateService = stateService;
        }

        // GET: Districts
        public async Task<IActionResult> Index()
        {
            var items = await _districtService.GetAllDistrictsAsync();
            return View(items);
        }

        // GET: Districts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var vm = await _districtService.GetDistrictByIdAsync(id.Value);

            if (vm == null)
                return NotFound();

            return View(vm);
        }

        // GET: Districts/Create
        public async Task<IActionResult> Create()
        {
            var states = await _stateService.GetAllStatesAsync();

            ViewData["StateId"] = new SelectList(
                states,
                "StateId",
                "StateName");

            return View(new DistrictViewModel());
        }

        // POST: Districts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DistrictViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _districtService.CreateDistrictAsync(vm);
                return RedirectToAction(nameof(Index));
            }

            var states = await _stateService.GetAllStatesAsync();

            ViewData["StateId"] = new SelectList(
                states,
                "StateId",
                "StateName",
                vm.StateId);

            return View(vm);
        }

        // GET: Districts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var vm = await _districtService.GetDistrictByIdAsync(id.Value);

            if (vm == null)
                return NotFound();

            var states = await _stateService.GetAllStatesAsync();

            ViewData["StateId"] = new SelectList(
                states,
                "StateId",
                "StateName",
                vm.StateId);

            return View(vm);
        }

        // POST: Districts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DistrictViewModel vm)
        {
            if (id != vm.DistrictId)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _districtService.UpdateDistrictAsync(vm);
                return RedirectToAction(nameof(Index));
            }

            var states = await _stateService.GetAllStatesAsync();

            ViewData["StateId"] = new SelectList(
                states,
                "StateId",
                "StateName",
                vm.StateId);

            return View(vm);
        }

        // GET: Districts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var vm = await _districtService.GetDistrictByIdAsync(id.Value);

            if (vm == null)
                return NotFound();

            return View(vm);
        }

        // POST: Districts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _districtService.DeleteDistrictAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}