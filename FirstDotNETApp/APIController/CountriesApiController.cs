using FirstDotNETApp.Filters;
using FirstDotNETApp.Interfaces;
using FirstDotNETApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FirstDotNETApp.APIController
{
    [ApiController]
    [Route("api/[controller]")]
    [TokenAuthorize]
    public class CountriesApiController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesApiController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _countryService.GetAllCountriesAsync();

            return Ok(countries);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CountryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _countryService.CreateCountryAsync(vm);

            return Ok(vm);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCountry([FromBody] CountryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _countryService.UpdateCountryAsync(vm);

            return Ok(vm);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await _countryService.DeleteCountryAsync(id);

            return Ok();
        }
    }

}