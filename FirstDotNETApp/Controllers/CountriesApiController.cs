using FirstDotNETApp.Interfaces;
using FirstDotNETApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FirstDotNETApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var countries =
                await _countryService.GetAllCountriesAsync();

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
    }

}