using FirstDotNETApp.Filters;
using FirstDotNETApp.Interfaces;
using FirstDotNETApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FirstDotNETApp.APIController
{
    [ApiController]
    [Route("api/[controller]")]
    [TokenAuthorize]
    public class MasterApiController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IStateService _stateService;
        private readonly IDistrictService _districtService;

    public MasterApiController(
        ICountryService countryService,
        IStateService stateService,
        IDistrictService districtService)
        {
            _countryService = countryService;
            _stateService = stateService;
            _districtService = districtService;
        }

        // Country APIs

        [HttpGet("Country/GetAll")]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            return Ok(countries);
        }

        [HttpPost("Country/Create")]
        public async Task<IActionResult> CreateCountry([FromBody] CountryViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _countryService.CreateCountryAsync(vm);
            return Ok(vm);
        }

        [HttpPatch("Country/Update")]
        public async Task<IActionResult> UpdateCountry([FromBody] CountryViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _countryService.UpdateCountryAsync(vm);
            return Ok(vm);
        }

        [HttpDelete("Country/Delete/{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await _countryService.DeleteCountryAsync(id);
            return Ok();
        }

        // State APIs

        [HttpGet("State/GetAll")]
        public async Task<IActionResult> GetAllStates()
        {
            var states = await _stateService.GetAllStatesAsync();
            return Ok(states);
        }

        [HttpPost("State/Create")]
        public async Task<IActionResult> CreateState([FromBody] StateViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _stateService.CreateStateAsync(vm);
            return Ok(vm);
        }

        [HttpPatch("State/Update")]
        public async Task<IActionResult> UpdateState([FromBody] StateViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _stateService.UpdateStateAsync(vm);
            return Ok(vm);
        }

        [HttpDelete("State/Delete/{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            await _stateService.DeleteStateAsync(id);
            return Ok();
        }

        // District APIs

        [HttpGet("District/GetAll")]
        public async Task<IActionResult> GetAllDistricts()
        {
            var districts = await _districtService.GetAllDistrictsAsync();
            return Ok(districts);
        }

        [HttpPost("District/Create")]
        public async Task<IActionResult> CreateDistrict([FromBody] DistrictViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _districtService.CreateDistrictAsync(vm);
            return Ok(vm);
        }

        [HttpPatch("District/Update")]
        public async Task<IActionResult> UpdateDistrict([FromBody] DistrictViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _districtService.UpdateDistrictAsync(vm);
            return Ok(vm);
        }

        [HttpDelete("District/Delete/{id}")]
        public async Task<IActionResult> DeleteDistrict(int id)
        {
            await _districtService.DeleteDistrictAsync(id);
            return Ok();
        }
    }
}
