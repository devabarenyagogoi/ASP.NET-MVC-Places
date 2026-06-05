using FirstDotNETApp.Filters;
using FirstDotNETApp.Interfaces;
using FirstDotNETApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FirstDotNETApp.APIController
{
    [ApiController]
    [Route("api/[controller]")]
    [TokenAuthorize]
    public class DistrictsApiController : ControllerBase
    {
        private readonly IDistrictService _districtService;

        public DistrictsApiController(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDistricts()
        {
            var districts = await _districtService.GetAllDistrictsAsync();
            return Ok(districts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDistrict([FromBody] DistrictViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _districtService.CreateDistrictAsync(vm);

            return Ok(vm);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateDistrict([FromBody] DistrictViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _districtService.UpdateDistrictAsync(vm);

            return Ok(vm);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistrict(int id)
        {
            await _districtService.DeleteDistrictAsync(id);

            return Ok();
        }
    }
}
