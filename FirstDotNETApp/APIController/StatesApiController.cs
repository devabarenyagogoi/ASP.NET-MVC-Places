using FirstDotNETApp.Filters;
using FirstDotNETApp.Interfaces;
using FirstDotNETApp.Services;
using FirstDotNETApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FirstDotNETApp.APIController
{
    [ApiController]
    [Route("api/[controller]")]
    [TokenAuthorize]
    public class StatesApiController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StatesApiController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet]
        //[]
        public async Task<IActionResult> GetAllStates()
        {
            var states = await _stateService.GetAllStatesAsync();

            return Ok(states);
        }

        [HttpPost]
        public async Task<IActionResult> CreateState([FromBody] StateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _stateService.CreateStateAsync(vm);

            return Ok(vm);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateState([FromBody] StateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _stateService.UpdateStateAsync(vm);

            return Ok(vm);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            await _stateService.DeleteStateAsync(id);

            return Ok();
        }
    }
}