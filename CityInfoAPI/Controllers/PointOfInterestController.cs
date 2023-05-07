using CityInfoAPI.Models;
using CityInfoAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PointOfInterestController:ControllerBase
    {
        private readonly IPointOfInterestService _pointOfInterestService;

        public PointOfInterestController(IPointOfInterestService pointOfInterestService)
        {
            _pointOfInterestService = pointOfInterestService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<PointOfInterestDto>>> GetPointsOfInterest()
        {
            return Ok(await _pointOfInterestService.GetPointsOfInterest());
        }

        [HttpGet]
        [Route("{pointOfInterestId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(Guid pointOfInterestId)
        {
            var pointOfInterest= await _pointOfInterestService.GetPointOfInterest(pointOfInterestId);

            if (pointOfInterest.PointOfInterestId == Guid.Empty)
                return NotFound();

            return Ok(pointOfInterest);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PointOfInterestDto>> AddPointOfInterest(PointOfInterestDto pointOfInterestDto)
        {
            var pointOfInterest = await _pointOfInterestService.AddPointOfInterest(pointOfInterestDto);
            if (pointOfInterest.PointOfInterestId == Guid.Empty)
                return NotFound($"City with id{pointOfInterestDto.CityId} was not found");

            return Ok(pointOfInterest);
        }

        [HttpPut]
        [Route("{pointOfInterestId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PointOfInterestDto>> UpdatePointOfInterest(Guid pointOfInterestId, PointOfInterestDto pointOfInterestDto)
        {
            var pointOfInterest = await _pointOfInterestService.UpdatePointOfInterest(pointOfInterestId, pointOfInterestDto);

            if (pointOfInterest.PointOfInterestId == Guid.Empty)
                return NotFound();

            return Ok(pointOfInterest);
        }

        [HttpDelete]
        [Route("{pointOfInterestId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePointOfInterest(Guid pointOfInterestId)
        {
            var pointOfInterest = await _pointOfInterestService.DeletePointOfInterest(pointOfInterestId);
            if (pointOfInterest.PointOfInterestId == Guid.Empty)
                return NotFound();

            return Ok();
        }
    }
}
