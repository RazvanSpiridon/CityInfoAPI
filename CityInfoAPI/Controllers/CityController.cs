using CityInfoAPI.Models;
using CityInfoAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<CityDto>>> GetCities()
        {
            return Ok(await _cityService.GetCities());
        }

        [HttpGet]
        [Route("GetCitiesWithPointsOfInterest")]
        [Authorize]
        public async Task<ActionResult<List<CityWithPointsOfInterestDto>>> GetCitiesWithPointsOfInterest()
        {
            return Ok(await _cityService.GetCitiesWithPointsOfInterest());
        }

        [HttpGet]
        [Route("GetCitiesWithPointsOfInterest/{cityId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<CityWithPointsOfInterestDto>>> GetCityWithPointsOfInterest(Guid cityId)
        {
            var city = await _cityService.GetCityWithPointsOfInterest(cityId);

            if (city.CityId == Guid.Empty)
                return NotFound($"City with id {cityId} do not exist");

            return Ok(city);
        }

        [HttpGet]
        [Route("{cityId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CityDto>> GetCity(Guid cityId)
        {
            var city = await _cityService.GetCity(cityId);
            if (city.CityId == Guid.Empty)
                return NotFound($"City with id {cityId} do not exist");

            return Ok(city);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CityDto>> AddCity(CityDto cityDto)
        {
            return Ok(await _cityService.AddCity(cityDto));
        }

        [HttpPut]
        [Route("{cityId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CityDto>> UpdateCity (Guid cityId, CityDto cityDto)
        {
            var city = await _cityService.UpdateCity(cityId, cityDto);

            if (city.CityId == Guid.Empty)
                return NotFound($"City with id {cityId} do not exist");

            return Ok(city);    
        }

        [HttpDelete]
        [Route("{cityId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCity(Guid cityId)
        {
            var city = await _cityService.DeleteCity(cityId);

            if (city.CityId == Guid.Empty)
                return NotFound($"City with id {cityId} do not exist");

            return Ok($"The city with id {cityId} was deleted");
        }


    }
}
